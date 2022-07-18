using System.Text.Json.Serialization;

namespace AppareVerstigium.Service;

public class CentrisClient
{
    private const string _centrisBaseUrl = "https://www.centris.ca/";
    private HttpClient _httpClient = new HttpClient();
    public async Task GetInscriptions(GetInscriptionsOptions options)
    { 
        var m = await _httpClient.PostAsJsonAsync(_centrisBaseUrl+"Property/GetInscriptions", options);
        var r = await m.Content.ReadFromJsonAsync<GetInscriptionsResult>();
        Console.WriteLine(r.Data.Result.Html);
    }
}

public class GetInscriptionsOptions 
{

    [JsonPropertyName("startPosition")]
    public int StartPosition { get; set; }
}

public class GetInscriptionsResult
{
    [JsonPropertyName("d")]
    public GetInscriptionsData Data { get; set; } = new GetInscriptionsData();

}
public class GetInscriptionsData
{
    [JsonPropertyName("Message")]
    public string Message { get; set; } = "";

    [JsonPropertyName("Result")]
    public InscriptionsResult Result { get; set; } = new InscriptionsResult();
    [JsonPropertyName("Succeeded")]
    public bool Succeeded { get; set; }

}

public class InscriptionsResult 
{
    [JsonPropertyName("html")]
    public string Html { get; set; } = "";
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("inscNumberPerPage")]
    public int InscNumberPerPage { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

}
