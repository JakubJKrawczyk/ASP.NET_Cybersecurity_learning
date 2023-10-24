

using System.Net;
using FrontApp.Handlers;

namespace FrontApp.Views;

public partial class ResetPasswordView : ContentPage
{
    private Entities.DataModels.User user;
    public ResetPasswordView()
    {
        InitializeComponent();
        user = GlobalProperties.PassParameters["User"] as Entities.DataModels.User;
    }

    private async void ChangeMyPassword(object sender, EventArgs e)
    {
        if (NewPasswordEntry.Text == NewPasswordRepeatEntry.Text)
        {
            //Check password
            if (!PasswordRequirements.CheckPassword(NewPasswordEntry.Text))
            {
                string message = "Your password is too weak! Password must contains:\n";
                if (PasswordRequirements.IsSmallLetters) message += "- Small letters\n";
                if (PasswordRequirements.IsBigLetters) message += "- Big letters\n";
                if (PasswordRequirements.IsSpecialLetters) message += "- Special letters\n";
                message += $"- Conajmniej {PasswordRequirements.PasswordLength} znaki";
                await DisplayAlert("Password Changed", message, "OK");
                return;
            }
            
            HttpHandler _handler = new();
            Entities.DataModels.User newUser = GlobalProperties.PassParameters["User"] as Entities.DataModels.User;
            newUser.Password = Hasher.Hasher.HashPassword(NewPasswordEntry.Text);
            HttpRequestMessage _request = new();
            HttpStatusCode _response;

            _request.RequestUri = new Uri($"{GlobalProperties.ApiUri}/users/user?login={newUser.Login}&token={GlobalProperties.UserToken}&password={newUser.Password}&expirationDate={(DateTime.Now + TimeSpan.FromDays(30)).ToString("yyyy-MM-dd")}&firstLogin={false}");
            _request.Method = HttpMethod.Put;
            _response = await _handler.HandlePut(_request);

            if (_response == HttpStatusCode.OK)
            {
                await DisplayAlert("Password Changed", "Your password has been changed", "OK");
                await Shell.Current.GoToAsync("///MainPage", true);
            }
            else
            {
                await DisplayAlert("Error", "Password Change Error! Something went wrong.", "Ohh");
            }
        }
        else await DisplayAlert("Password error", "Hasła nie jest zgodne! Sprawdź je i spróbuj ponownie", "OK");
    }
}