using System.Net;
using FrontApp.Handlers;

namespace FrontApp.Views;
 
public partial class LoginView : ContentPage
{
    private readonly HttpHandler _http;
    public LoginView()
    {
        InitializeComponent();
        _http = new HttpHandler();
        
    }

    private async void LoginUser(object sender, EventArgs e)
    {

        HttpRequestMessage request = new();

        request.RequestUri = new Uri($"{GlobalProperties.ApiUri}/users/auth?login={LoginEntry.Text}&password={Hasher.Hasher.HashPassword(PasswordEntry.Text)}");
        
        
        HttpResponseMessage response = await _http.HandlePost(request);
        
       
        
        if (response.StatusCode == HttpStatusCode.OK)
        {
            GlobalProperties.IsLogged = true;
            GlobalProperties.UserToken = response.Content.ReadAsStringAsync().Result;
            GlobalProperties.UserLogin = LoginEntry.Text;
            await Shell.Current.GoToAsync("///MainPage");
            
        }
        else
        {
            ResponseMessageLabel.Text = "Złe dane logowania. Sprawdź je i spróbuj ponownie";
        }
    }
}
class userHttp
{
    public string login { get; set; }
    public string password { get; set; }
}