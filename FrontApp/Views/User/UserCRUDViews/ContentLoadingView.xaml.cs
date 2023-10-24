using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontApp.Views.User.UserCRUDViews;

public partial class ContentLoadingView : ContentPage
{
    public ContentLoadingView()
    {
        InitializeComponent();
        Shell.Current.GoToAsync("///MainPage");
    }
}