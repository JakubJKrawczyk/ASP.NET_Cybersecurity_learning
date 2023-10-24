using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FrontApp.Handlers;

namespace FrontApp.Views.User.UserCRUDViews;

public partial class AddUserView : ContentPage
{
    public AddUserView()
    {
        InitializeComponent();
    }


    private async void AddUser(object sender, EventArgs e)
    {
        if (PasswordEntry.Text == RepeatPasswordEntry.Text)
        {
            if (!PasswordRequirements.CheckPassword(PasswordEntry.Text)) return;
            HttpHandler handler = new();
            HttpRequestMessage request = new();
            HttpResponseMessage response;

            request.RequestUri =
                new Uri(
                    $"{GlobalProperties.ApiUri}/users/user?login={LoginEntry.Text}&password={Hasher.Hasher.HashPassword(PasswordEntry.Text)}&token={GlobalProperties.UserToken}");
            response = await handler.HandlePost(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                DisplayAlert("User Added", "User has been added!", "OK");
                
            }
            else
            {
                DisplayAlert("User Added Error", $"ERROR: StatusCode: {response.StatusCode}", "OK");
            }
        }
        
    }
}