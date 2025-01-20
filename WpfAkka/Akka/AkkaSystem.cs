using Akka.Actor;
using Serilog;
using WpfAkka.ViewModels;

namespace WpfAkka.Actors;

internal static class AkkaSystem
{
    #region [ Initialize ]
    private static readonly ActorSystem RootSystem = BuildSystem();
    private static Dictionary<int, string> PluginAddresses = [];

    private static ActorSystem BuildSystem()
    {
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/akka-trace.log", rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .CreateLogger();

        // Create the ActorSystem with Serilog integration
        var config = @"
            akka {
                loglevel = DEBUG
                loggers = [""Akka.Logger.Serilog.SerilogLogger, Akka.Logger.Serilog""]
            }
        ";

        return ActorSystem.Create("RootSystem", config);
    }

    public static void Initialize(IEnumerable<PluginViewModel> plugins)
    {
        foreach (var item in plugins)
        {
            IActorRef pluginActor = RootSystem.ActorOf(Props.Create(() => new PluginActor(item)), PluginActor.BuildName(item.Id));
            PluginAddresses[item.Id] = pluginActor.Path.ToString();
        }
    }
    #endregion

    #region [ Use ]
    public static ActorSystem Instance => RootSystem;

    public static void Send(object message, int? pluginId)
    {
        if (pluginId.HasValue)
        {
            var path = PluginActor.BuildPath(pluginId.Value);
            var actor = RootSystem.ActorSelection(path);
            actor.Tell(message);
            return;
        }

        // broadcast to all plugins
        var selection = RootSystem.ActorSelection(PluginActor.BuildPath());
        selection.Tell(message);
    }
    #endregion
}
