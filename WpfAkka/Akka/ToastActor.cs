using Akka.Actor;
using Akka.Event;
using WpfAkka.Models.Messages;
using WpfAkka.ViewModels;

namespace WpfAkka.Actors;

internal class ToastActor : ReceiveActor
{
    private readonly ILoggingAdapter _log = Context.GetLogger();
    private readonly ToastViewModel _viewModel;

    public ToastActor(ToastViewModel viewModel)
    {
        _viewModel = viewModel;

        Receive((Action<PatientOpenedInEMR>)(message =>
        {
            _log.Debug($"Received 'message:{message.Id}'");

            // process the message
            var ms = new Random().Next(100, 500);
            Thread.Sleep(ms);

            // update the view model
            _viewModel.OnMessageProcessed(message);
        }));
    }
}
