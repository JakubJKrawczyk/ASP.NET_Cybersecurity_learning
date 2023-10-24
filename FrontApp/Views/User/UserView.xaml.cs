using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrontApp.Views.User;

namespace FrontApp.Views;

public partial class UserView : ContentPage
{
    public UserView()
    {
        InitializeComponent();
    }

    private void ToChangeMyPassword(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(ChangeUserPasswordView));
    }
}