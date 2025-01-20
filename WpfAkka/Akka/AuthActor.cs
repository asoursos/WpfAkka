using Akka.Actor;
using Akka.Event;
using WpfAkka.Models.Messages;
using WpfAkka.ViewModels;

namespace WpfAkka.Actors;

internal class AuthActor : ReceiveActor
{
    private readonly ILoggingAdapter _log = Context.GetLogger();
    private readonly AuthViewModel _viewModel;

    public AuthActor(AuthViewModel viewModel)
    {
        _viewModel = viewModel;
        Become(Unauthenticated);
    }

    private void Unauthenticated()
    {
        Receive<UserLoggingIn>(msg =>
        {
            // auth magic, login the user.
            Thread.Sleep(1000);

            Become(Authenticated);
            Context.Parent.Tell(new UserLoggedIn { Username = msg.Username });
            _viewModel.OnMessageProcessed(msg);
            _viewModel.OnUserLoggedIn(msg);
        });
    }

    private void Authenticated()
    {
        Receive<UserLoggingOut>(msg =>
        {
            Thread.Sleep(1000);

            Become(Unauthenticated);
            Context.Parent.Tell(new UserLoggedOut { Username= msg.Username });
            _viewModel.OnMessageProcessed(msg);
            _viewModel.OnUserLoggedOut(msg);
        });
    }
}
