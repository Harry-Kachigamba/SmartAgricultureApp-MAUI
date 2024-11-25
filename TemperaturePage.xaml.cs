using SmartAgricultureApp_MAUI.Services;
using System.Timers;

namespace SmartAgricultureApp_MAUI
{
    public partial class TemperaturePage : ContentPage
    {
        private TemperatureService _temperatureService;
        private System.Timers.Timer _timer;
    
        public TemperaturePage()
        {
            InitializeComponent();
            _temperatureService = new TemperatureService();

            _timer = new System.Timers.Timer(200);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }
        
        private async void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var recentTemperature = await _temperatureService.GetRecentTemperature();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                TemperatureLabel.Text = $"{recentTemperature?.ToString("F1") ?? "Loading..."} �C";
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
}
