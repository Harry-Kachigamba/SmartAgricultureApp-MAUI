using SmartAgricultureApp_MAUI.Services;
using System.Timers;

namespace SmartAgricultureApp_MAUI;

public partial class MoisturePage : ContentPage
{
    private MoistureService _moistureService;
    private System.Timers.Timer _timer;
    public MoisturePage()
    {
        InitializeComponent();
        
        _moistureService = new MoistureService();
        _timer = new System.Timers.Timer(5000);
        _timer.Elapsed += OnTimerElapsed;
        _timer.Start();
    }
    
    private async void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        var recentMoisture = await _moistureService.GetRecentMoisture();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            MoistureLabel.Text = $"{recentMoisture?.ToString("F2") ?? "Loading... "} %";
        });
    }

    protected override void OnDisappearing()
    {
        _timer.Stop();
        base.OnDisappearing();
    }

    protected override void OnAppearing()
    {
        _timer.Start();
        base.OnAppearing();
    }
}