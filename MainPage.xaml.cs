using SmartAgricultureApp_MAUI.Services;

namespace SmartAgricultureApp_MAUI;

public partial class MainPage : ContentPage
{
    private readonly SupabaseServiceClass _supabaseService;
    
    public MainPage()
    {
        InitializeComponent();
        _supabaseService = new SupabaseServiceClass();
    }
    
    //Navigation Button to Moisture Page
    private async void OnClickedMoistureButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MoisturePage());
    }
    
    // Navigation Button to Temperature Page
    private async void OnClickedTemperatureButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TemperaturePage());
    }

    // Navigation Button to Humidity Page
    private async void OnClickedHumidityButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HumidityPage());
    }

    // Navigation Button to Plant Health Page
    private async void OnClickedPlantHealthButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlantHealthPage());
    }

    // Navigation Button to Notification Page
    private async void OnClickedNotificationButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotificationPage());
    }

    // Navigation Page to History Page
    private async void OnClickedHistoryButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HistoryPage());
    }

    // Signout label link
    private async void OnClickedSignout(object sender, TappedEventArgs e)
    {
        try
        {
            var result = await _supabaseService.SignOutUser();
            await DisplayAlert("Message", "Do you want to logout?", "OK");
            if (result)
            {
                await Navigation.PushAsync(new SignInPage());
            }
        }
        catch
        {
            await Navigation.PopAsync();
        }
    }
}