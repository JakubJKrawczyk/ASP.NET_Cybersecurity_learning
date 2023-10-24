using FrontApp.Views;

namespace FrontApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        //Global properties
        PagesHolder.Pages = new();
        
        
        MainPage = new AppShell();
        
        
    }
}