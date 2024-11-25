using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAgricultureApp_MAUI.Services;

namespace SmartAgricultureApp_MAUI;

public partial class SignUpPage : ContentPage
{
    private readonly SupabaseServiceClass _supabaseService;
    
    public SignUpPage()
    {
        InitializeComponent();
        _supabaseService = new SupabaseServiceClass();
    }
    
    private async void OnLoginTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignInPage());
    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        try
        {
            string result = await _supabaseService.SignUpUser(email, password);
            await DisplayAlert("Message", "You have successfully signed up", "OK");

            await Task.Delay(1000);
            await Navigation.PushAsync(new SignInPage());
        }

        catch
        {
            await DisplayAlert("Alert", "Sign up unsuccessful", "OK");
        }
    }
}