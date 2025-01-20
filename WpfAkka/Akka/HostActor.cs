using Akka.Actor;
using Akka.Event;
using WpfAkka.Models.Messages;
using WpfAkka.ViewModels;

namespace WpfAkka.Actors;

internal class HostActor : ReceiveActor
{
    internal const string HostActorName = "host";
    internal const string HostActorPath = "/user/host";

    private readonly ActorPath _authActorPath;
    private readonly ActorPath _aresActorPath;
    private readonly ActorPath _toastActorPath;

    private readonly ILoggingAdapter _log = Context.GetLogger();
    private readonly HostViewModel _viewModel;

    public HostActor(HostViewModel viewModel)
    {
        _viewModel = viewModel;

        var auth = Context.ActorOf(Props.Create(() => new AuthActor(viewModel.AuthViewModel)), "auth");
        _authActorPath = auth.Path;

        var ares = Context.ActorOf(Props.Create(() => new AresActor(viewModel.AresViewModel)), "ares");
        _aresActorPath = ares.Path;
        
        var toast = Context.ActorOf(Props.Create(() => new ToastActor(viewModel.ToastViewModel)), "toast");
        _toastActorPath = toast.Path;

        Receive<UserLoggingIn>(msg => Context.ActorSelection(_authActorPath).Tell(msg));
        Receive<UserLoggingOut>(msg => Context.ActorSelection(_authActorPath).Tell(msg));
     
        Receive<PatientOpenedInEMR>(OnPatientOpened);
    }

    #region [ Router ]
    
    private void OnPatientOpened(PatientOpenedInEMR msg)
    {
        if (_viewModel.IsMainWindowOpened)
        {
            Context.ActorSelection(_aresActorPath).Tell(msg);
        }
        else
        {
            Context.ActorSelection(_toastActorPath).Tell(msg);
        }
    }
    #endregion
}
