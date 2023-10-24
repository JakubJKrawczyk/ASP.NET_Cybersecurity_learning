using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FrontApp.Handlers;
using FrontApp.Views.User.UserCRUDViews;
using Newtonsoft.Json;

namespace FrontApp.Views.Admin;

public partial class UsersListView : ContentPage
{
    public List<Entities.DataModels.User> Users { get; set; }
    public UsersListView()
    {
        InitializeComponent();

    }

    private void ToAddUser(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddUserView));
    }

    private void ToUserDetails(object sender, EventArgs e)
    {
        Console.WriteLine(UsersList.SelectedItem.ToString());

        if (!GlobalProperties.PassParameters.Keys.ToList().Contains("User"))
        {
            GlobalProperties.PassParameters.Add("User", UsersList.SelectedItem as Entities.DataModels.User);
        }
        else
        {
            GlobalProperties.PassParameters["User"] = UsersList.SelectedItem as Entities.DataModels.User;
        }

        Shell.Current.GoToAsync(nameof(DetailUserView));
    }

    private async void RefreshUsersList(object sender, NavigatedToEventArgs navigatedToEventArgs)
    {
        HttpHandler handler = new();
        HttpRequestMessage request = new();
        HttpResponseMessage response;
        request.RequestUri = new Uri($"{GlobalProperties.ApiUri}/users?token={GlobalProperties.UserToken}");
        request.Method = HttpMethod.Get;

        response = await handler.HandleGet(request);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Users = JsonConvert.DeserializeObject<List<Entities.DataModels.User>>(
                await response.Content.ReadAsStringAsync());

            if (Users is not null)
            {
                
                List<string> usersNames = Users.Select(u => u.Login).ToList();

                UsersList.ItemsSource = Users;
            }
        }
        
    }
}