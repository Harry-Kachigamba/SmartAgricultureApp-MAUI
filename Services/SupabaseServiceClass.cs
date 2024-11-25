using Supabase;

namespace SmartAgricultureApp_MAUI.Services
{

    public class SupabaseServiceClass
    {
        public readonly string? _supabaseUrl;
        public readonly string? _supabaseKey;
        public readonly Client _supabaseClient;

        //Method to get the set environment variables
        public SupabaseServiceClass()
        {
            _supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL");
            _supabaseKey = Environment.GetEnvironmentVariable("SUPABASE_KEY");

            //Checking if key values are available
            if (string.IsNullOrWhiteSpace(_supabaseUrl) || string.IsNullOrWhiteSpace(_supabaseKey))
            {
                throw new InvalidCastException("Supabase URL or API Key is missing");
            }

            //Setting the keys to a variable
            _supabaseClient = new Client(_supabaseUrl, _supabaseKey);
        }

        //Signup configuration method
        public async Task<string> SignUpUser(string email, string password)
        {
            var authSignUp = _supabaseClient.Auth;
            var response = await authSignUp.SignUp(email, password);

            if (response == null)
            {
                return "User Signed Up Successfully";
            }

            else
            {
                return $"Error signing up user: {response?.ToString()}";
            }
        }

        //Signin configuration method
        public async Task<string> SignInUser(string email, string password)
        {
            var authSignIn = _supabaseClient.Auth;
            var response = await authSignIn.SignIn(email, password);

            if (response == null)
            {
                return "User Signed In Successfully";
            }

            else
            {
                return $"Error signing in user: {response?.ToString()}";
            }
        }

        public async Task<bool> SignOutUser()
        {
            await _supabaseClient.Auth.SignOut();
            return true;
        }
    }
}