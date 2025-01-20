using CommunityToolkit.Mvvm.ComponentModel;
using WpfAkka.Models.Messages;

namespace WpfAkka.ViewModels;

internal partial class AuthViewModel : PluginViewModel
{
    [ObservableProperty]
    private string status;

    internal void OnUserLoggedIn(UserLoggingIn msg)
    {
        Status = "Logged In";
    }

    internal void OnUserLoggedOut(UserLoggingOut msg)
    {
        Status = "Logged Out";
    }
}
