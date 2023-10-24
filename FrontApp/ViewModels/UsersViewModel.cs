using System.Collections.ObjectModel;
using Entities.DataModels;

namespace FrontApp.ViewModels;

public class UsersViewModel
{
    public ObservableCollection<User> UsersView { get; set; }
}