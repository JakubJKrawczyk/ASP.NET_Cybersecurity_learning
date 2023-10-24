using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontApp.Handlers;
using FrontApp.Views.Admin;
using Newtonsoft.Json;

namespace FrontApp.Views;

public partial class AdminView : ContentPage
{
    
    public  AdminView()
    {
        InitializeComponent();
        GlobalProperties.PassParameters.Clear();
        SetGlobalUser();
    }

    private async void SetGlobalUser()
    {
        HttpHandler _handler = new();

        HttpRequestMessage _request = new();
        HttpResponseMessage _response = new();

        _request.RequestUri = new Uri($"{GlobalProperties.ApiUri}/users/user/{GlobalProperties.UserLogin}?token={GlobalProperties.UserToken}");
        _request.Method = HttpMethod.Get;

        _response = await _handler.HandleGet(_request);

        Entities.DataModels.User _user =  JsonConvert.DeserializeObject<Entities.DataModels.User>(await _response.Content.ReadAsStringAsync());
        GlobalProperties.PassParameters.Add("User", _user);
    }
    private void ToChangeAdminPassword(object sender, EventArgs e)
    {
        
        Shell.Current.GoToAsync(nameof(ChangeAdminPasswordView), true);
    }
    
    private void ToUsersList(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(UsersListView), true);
        }
    private void ToPasswordRequirements(object sender, EventArgs e)
            {
                Shell.Current.GoToAsync(nameof(PasswordRequirementsView), true);
            }
    
}