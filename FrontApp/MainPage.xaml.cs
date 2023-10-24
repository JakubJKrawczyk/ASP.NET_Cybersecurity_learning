using Entities.DataModels;
using FrontApp.Handlers;
using FrontApp.Views;
using FrontApp.Views.Admin;
using FrontApp.Views.User;
using Newtonsoft.Json;

namespace FrontApp;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        
        
    }

    

    private async void CheckIfLogged(object sender, EventArgs e)
    {
        if (!GlobalProperties.IsLogged)
        {
            Console.Write("Użytkownik niezalogowany\n");
            await Shell.Current.GoToAsync(nameof(LoginView));
            
        }
        else
        {
            Console.WriteLine("Zalogowany\n");
            CheckTabsVisible();
            if (GlobalProperties.UserLogin is not null && GlobalProperties.UserLogin != "")
            {
                WelcomeLabel.Text = $"Welcome {GlobalProperties.UserLogin}";
                bool _first = await CheckIfLoggedFirstTime();
                if (!_first)
                {
                   await CheckIfPasswordExpires();
                }
                
                
            }
            
        }

        LogoutBtn.IsVisible = GlobalProperties.IsLogged ;
    }

    private async Task<bool> CheckIfLoggedFirstTime()
    {
        HttpHandler _handler = new();

        HttpRequestMessage _request = new();
        HttpResponseMessage _response = new();

        _request.RequestUri = new Uri($"{GlobalProperties.ApiUri}/users/user/{GlobalProperties.UserLogin}?token={GlobalProperties.UserToken}");
        _request.Method = HttpMethod.Get;

        _response = await _handler.HandleGet(_request);

        User user =  JsonConvert.DeserializeObject<User>(await _response.Content.ReadAsStringAsync());
        if (user.FirstLogin)
        {
            if (!GlobalProperties.PassParameters.ContainsKey("User"))
            {
                GlobalProperties.PassParameters.Add("User", user);
            }
            else GlobalProperties.PassParameters["User"] = user;
            
            await Shell.Current.GoToAsync(nameof(ResetPasswordView));
            return true;
        }
        else return false;
    }

    private async Task<bool> CheckIfPasswordExpires()
    {
        HttpHandler _handler = new();

        HttpRequestMessage _request = new();
        HttpResponseMessage _response = new();

        _request.RequestUri = new Uri($"{GlobalProperties.ApiUri}/users/user/{GlobalProperties.UserLogin}?token={GlobalProperties.UserToken}");
        _request.Method = HttpMethod.Get;

        _response = await _handler.HandleGet(_request);

        User user =  JsonConvert.DeserializeObject<User>(await _response.Content.ReadAsStringAsync());

        if (DateTime.Now > user.PasswordExpirationDate)
        {
            if (!GlobalProperties.PassParameters.ContainsKey("User"))
            {
                GlobalProperties.PassParameters.Add("User", user);
            }
            else GlobalProperties.PassParameters["User"] = user;
                
            await Shell.Current.GoToAsync(nameof(ChangeUserPasswordView));
            return true;
        } else return false;
    }
    public async void CheckTabsVisible()
    {
                HttpHandler _handler = new();
                HttpRequestMessage _request = new();
                _request.RequestUri = new Uri(GlobalProperties.ApiUri + $"/users/privileges?token={GlobalProperties.UserToken}");
                _request.Content = null;
                HttpResponseMessage _response;

                try
                {

                     _response = await _handler.HandleGet(_request);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                if (_response is null) return;
                string role = await _response.Content.ReadAsStringAsync();
                if (role == "User")
                {

                    ToUserBtn.IsVisible = true;
                    ToAdminBtn.IsVisible = false;

                }else if (role == "Administrator")
                {
                    ToAdminBtn.IsVisible = true;
                    ToUserBtn.IsVisible = false;
                }
        
    }


    private void ToAdminPanel(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AdminView));
    }

    private void ToUserPanel(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(UserView));
    }

    private async void Logout(object sender, EventArgs e)
    {
        GlobalProperties.IsLogged = false;
        GlobalProperties.UserToken = "";
        
            
            Shell.Current.GoToAsync(nameof(LogoutView));
            
            Shell.Current.GoToAsync("///MainPage");
    }
}