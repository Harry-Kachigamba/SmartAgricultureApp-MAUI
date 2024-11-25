using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAgricultureApp_MAUI.Services;

namespace SmartAgricultureApp_MAUI;

public partial class SignInPage : ContentPage
{
    private readonly SupabaseServiceClass _supabaseServiceLogin;
    
    public SignInPage()
    {
        InitializeComponent();
        _supabaseServiceLogin = new SupabaseServiceClass();
    }
    
    private async void OnSignUpTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string emailLogin = LoginEmailEntry.Text;
        string passwordLogin = LoginPasswordEntry.Text;

        try
        {
            string resultLogin = await _supabaseServiceLogin.SignInUser(emailLogin, passwordLogin);
            await DisplayAlert("Message", "Successfully signed in", "OK");

            await Task.Delay(1000);
            await Navigation.PushAsync(new MainPage());
        }

        catch
        {
            await DisplayAlert("Alert", "Sign in unsuccessful", "OK");
        }
    }
}