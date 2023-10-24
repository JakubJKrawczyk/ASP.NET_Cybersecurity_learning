using System.Net;
using FrontApp.Handlers;

namespace FrontApp.Views.User.UserCRUDViews;

public partial class ModifyUserView : ContentPage
{
    private Entities.DataModels.User _user;
    public ModifyUserView()
    {
        InitializeComponent();
        _user = GlobalProperties.PassParameters["User"] as Entities.DataModels.User;
        SetFieldsData();
    }

    private void SetFieldsData()
    {
        LoginEntry.Text = _user.Login;
        FirstLoginCheckBox.IsChecked = _user.FirstLogin;
        
    }
    private void ShowChangeUserPassword(object sender, EventArgs e)
    {
        
        ShowPasswordEntriesBtn.IsVisible = false;

        NewPasswordEntry.IsVisible = true;
        RepeatNewPasswordEntry.IsVisible = true;
    }

    private async void ModifyUser(object sender, EventArgs e)
    {
        
        HttpHandler handler = new();
        HttpRequestMessage request = new();
        HttpStatusCode response;
        bool isEqual = true;
        request.RequestUri = new Uri(
            $"{GlobalProperties.ApiUri}/users/user?login={LoginEntry.Text}&token={GlobalProperties.UserToken}&password={(!NewPasswordEntry.IsVisible ? _user.Password : NewPasswordEntry.Text == RepeatNewPasswordEntry.Text ? Hasher.Hasher.HashPassword(NewPasswordEntry.Text) : () => {
                DisplayAlert("Error", "password must be the same!. check and repeat action", "OK");
                isEqual = false;
            })}&firstLogin={FirstLoginCheckBox.IsChecked}&expirationDate={(DateTime.Now + PasswordRequirements.PasswordExpiration).ToString("yyyy-MM-dd")}");
        if (!isEqual) return;
        if (!PasswordRequirements.CheckPassword(NewPasswordEntry.Text)) return;
        response = await handler.HandlePut(request);

        if (response == HttpStatusCode.OK)
        {
            DisplayAlert("User Modified", "User has been modified!", "OK");
        }
        else
        {
            DisplayAlert("User Modified Error", $"Error: StatusCode: {response}", "OK");
        }

        

    }
}