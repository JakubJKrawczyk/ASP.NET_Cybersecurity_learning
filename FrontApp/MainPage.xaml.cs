namespace FrontApp;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();

        if (!GlobalProperties.IsLogged)
        {
            Shell.Current.GoToAsync("login");
        }
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("login");
    }
}