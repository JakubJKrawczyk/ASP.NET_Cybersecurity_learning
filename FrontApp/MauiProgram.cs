using FrontApp.Views;
using FrontApp.Views.Admin;
using FrontApp.Views.User;
using FrontApp.Views.User.UserCRUDViews;
using Microsoft.Extensions.Logging;

namespace FrontApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<LoginView>();
        builder.Services.AddTransient<AdminView>();
        builder.Services.AddTransient<UserView>();
        builder.Services.AddTransient<LogoutView>();
        builder.Services.AddTransient<ChangeAdminPasswordView>();
        builder.Services.AddTransient<UsersListView>();
        builder.Services.AddTransient<PasswordRequirementsView>();
        builder.Services.AddTransient<ChangeUserPasswordView>();
        builder.Services.AddTransient<ResetPasswordView>();
        builder.Services.AddTransient<DetailUserView>();
        builder.Services.AddTransient<AddUserView>();
        builder.Services.AddTransient<ModifyUserView>();
        builder.Services.AddTransient<ContentLoadingView>();
        
        
        Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        Routing.RegisterRoute(nameof(AdminView), typeof(AdminView));
        Routing.RegisterRoute(nameof(UserView), typeof(UserView));
        Routing.RegisterRoute(nameof(LogoutView), typeof(LogoutView));
        Routing.RegisterRoute(nameof(ContentLoadingView), typeof(ContentLoadingView));
        
        //Admin Views
        Routing.RegisterRoute(nameof(ChangeAdminPasswordView), typeof(ChangeAdminPasswordView));
        Routing.RegisterRoute(nameof(UsersListView), typeof(UsersListView));
        Routing.RegisterRoute(nameof(PasswordRequirementsView), typeof(PasswordRequirementsView));
        Routing.RegisterRoute(nameof(DetailUserView), typeof(DetailUserView));
        Routing.RegisterRoute(nameof(ModifyUserView), typeof(ModifyUserView));
        Routing.RegisterRoute(nameof(AddUserView), typeof(AddUserView));
        
        //User Views
        Routing.RegisterRoute(nameof(ChangeUserPasswordView), typeof(ChangeUserPasswordView));
        Routing.RegisterRoute(nameof(ResetPasswordView), typeof(ResetPasswordView));
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}