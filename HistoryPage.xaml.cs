using SmartAgricultureApp_MAUI.Services;

namespace SmartAgricultureApp_MAUI;

public partial class HistoryPage : ContentPage
{
    private HistoryPageService _historyPageService;
    
    public HistoryPage()
    {
        InitializeComponent();

        _historyPageService = new HistoryPageService();
        LoadRecentTemperature();
    }
    
    private async void LoadRecentTemperature()
    {
        var temperatures = await _historyPageService.GetRecentTemperature(5);
        TemperatureListView.ItemsSource = temperatures;
    }

    private async void LoadRecentHumidity()
    {
        var humidity = await _historyPageService.GetRecentHumidity(5);
        HumidityListView.ItemsSource = humidity;
    }

    private async void LoadRecentMoisture()
    {
        var moisture = await _historyPageService.GetRcentMoisture(5);
        MoistureListView.ItemsSource = moisture;
    }
}