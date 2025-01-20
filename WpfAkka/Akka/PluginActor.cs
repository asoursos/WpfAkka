using Akka.Actor;
using Akka.Event;
using System.Windows.Threading;
using WpfAkka.Models;
using WpfAkka.ViewModels;

namespace WpfAkka.Actors;

internal class PluginActor : ReceiveActor
{
    // https://getakka.net/articles/actors/dispatchers.html 

    private readonly ILoggingAdapter _log = Context.GetLogger();
    private readonly PluginViewModel _viewModel;

    internal static string BuildName(int id) => $"plugin-{id}";
    internal static string BuildPath(int? id = null) => id.HasValue ? $"/user/plugin-{id}" : $"/user/plugin-*";

    public PluginActor(PluginViewModel viewModel)
    {
        _viewModel = viewModel;

        Receive((Action<PatientContextChanged>)(message =>
        {
            _log.Debug($"Received 'message:{message.Id}'");

            // process the message
            var ms = new Random().Next(100, 500);
            Thread.Sleep(ms);

            // update the view model
            _viewModel.OnMessageProcessed(message);
        }));
    }

    protected override void PreStart()
    {
        _log.Info($"PluginActor:{this.Self} started");
        base.PreStart();
    }

    protected override void PostStop()
    {
        _log.Info($"PluginActor:{this.Self} stopped");
        base.PostStop();
    }
}
