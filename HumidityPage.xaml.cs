using SmartAgricultureApp_MAUI.Services;
using System.Timers;

namespace SmartAgricultureApp_MAUI;

public partial class HumidityPage : ContentPage
{
    private HumidityService _humidityService;
    private System.Timers.Timer _timer;
    
    public HumidityPage()
    {
        InitializeComponent();
        
        _humidityService = new HumidityService();
        _timer = new System.Timers.Timer(5000);
        _timer.Elapsed += OnTimerElapsed;
        _timer.Start();
    }
    
    private async void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        var recentHumidity = await _humidityService.GetRecentHumidity();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            HumidityLabel.Text = $"{recentHumidity?.ToString("F1") ?? "Loading..."}";
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