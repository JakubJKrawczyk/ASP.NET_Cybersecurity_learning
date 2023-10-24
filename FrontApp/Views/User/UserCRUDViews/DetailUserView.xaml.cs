using System.Net;
using Entities.DataModels;
using FrontApp.Handlers;
using Newtonsoft.Json;

namespace FrontApp.Views.User.UserCRUDViews;

public partial class DetailUserView : ContentPage
{
    private Entities.DataModels.User _user;
    public DetailUserView()
    {
        InitializeComponent();
        _user = GlobalProperties.PassParameters["User"] as Entities.DataModels.User;
        SetFieldsData();

    }


    private async void SetFieldsData()
    {
        LoginLabel.Text = "Login: " + _user.Login;
        PasswordLabel.Text = "Password: " + _user.Password;
        FirstLoginLabel.Text = "FirstLogin: " + _user.FirstLogin.ToString();
        PasswordExpiresLabel.Text = "Password expiration date: " + _user.PasswordExpirationDate.ToString("yyyy-MM-dd");

        HttpHandler handler = new();
        HttpRequestMessage request = new();
        HttpResponseMessage response;

        request.RequestUri = new Uri($"{GlobalProperties.ApiUri}/Role/role?token={GlobalProperties.UserToken}&roleid={_user.RoleId}");
        response = await handler.HandleGet(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Role role = JsonConvert.DeserializeObject<Role>(await response.Content.ReadAsStringAsync());

            RoleLabel.Text = "Role name: " + role.Name;
        }
    }
    

    private void ToModifyUser(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(ModifyUserView));
    }

    private async void DeleteUser(object sender, EventArgs e)
    {
        HttpHandler handler = new();
        HttpRequestMessage request = new();
        HttpResponseMessage response;

        request.RequestUri = new Uri($"{GlobalProperties.ApiUri}/users/user?token={GlobalProperties.UserToken}&login={_user.Login}");
        response = await handler.HandleDelete(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            DisplayAlert("User Deleted", "User has been deleted!", "OK");
            Shell.Current.GoToAsync("///MainPage");
        }
        else
        {
            DisplayAlert("User Deleted Error", $"ERROR: Status Code: {response.StatusCode}", "OK");
        }
    }
}