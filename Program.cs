using AppareVerstigium.Service.Interface;
using AppareVerstigium.Service;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var scrapper = new CentrisScrapper(builder.Configuration, new CentrisClient());
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigins",
    policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// DI
builder.Services.AddSingleton<IScrapper>(new CentrisScrapper(builder.Configuration, new CentrisClient()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); 

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAllOrigins");
app.MapGet("", ([FromServices] IScrapper scrapper) =>
{
    scrapper.Start();
});

app.Run();
