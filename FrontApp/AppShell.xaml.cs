using FrontApp.Views;

namespace FrontApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute("login", typeof(LoginView));
        Routing.RegisterRoute("main", typeof(MainPage));
    }
}