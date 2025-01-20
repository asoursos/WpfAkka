using CommunityToolkit.Mvvm.ComponentModel;
using WpfAkka.Models.Messages;

namespace WpfAkka.ViewModels;

internal partial class AuthViewModel : PluginViewModel
{
    [ObservableProperty]
    private string status;

    internal override void OnMessageProcessed(BaseEvent message)
    {
        base.OnMessageProcessed(message);
        if (message.GetType() == typeof(UserLoggedIn))
        {
        }
        else if (message.GetType() == typeof(UserLoggedOut))
        {
            Status = "Logged Out";
        }
    }

    internal void OnUserLoggedIn(UserLoggingIn msg)
    {
        Status = "Logged In";
    }

    internal void OnUserLoggedOut(UserLoggingOut msg)
    {
        Status = "Logged Out";
    }
}
