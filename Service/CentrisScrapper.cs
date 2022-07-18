using AppareVerstigium.Service.Interface;
using System.Timers;
namespace AppareVerstigium.Service;
public class CentrisScrapper : IScrapper
{
    private CentrisClient _centrisClient;
    private IConfiguration _configuration;
    public System.Timers.Timer Timer = null!;
    public State State { get; private set; }
    public CentrisScrapper(IConfiguration configuration, CentrisClient client)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _centrisClient = client ?? throw new ArgumentNullException(nameof(client));
        State = State.Idle;
        Timer = new System.Timers.Timer(TimeSpan.FromSeconds(5).TotalMilliseconds);
        Timer.Elapsed += DoWorkAsync;
        
    }
    public void Start() 
    {
        Timer.Start();
        State = State.Running;
    }

    private async void DoWorkAsync(object? sender, ElapsedEventArgs e)
    {
        Console.WriteLine("Running...");
        await _centrisClient.GetInscriptions(new GetInscriptionsOptions { StartPosition = 20 });

    }
}
