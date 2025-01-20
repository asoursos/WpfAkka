using Akka.Actor;
using Serilog;
using WpfAkka.ViewModels;

namespace WpfAkka.Actors;

internal static class AkkaSystem
{
    #region [ Initialize ]
    private static readonly ActorSystem RootSystem = BuildSystem();
    private static Dictionary<int, string> PluginAddresses = [];
    private static ActorPath _hostActorPath;

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

    public static void Initialize(HostViewModel viewModel)
    {
        IActorRef hostActor = RootSystem.ActorOf(Props.Create(() => new HostActor(viewModel)), HostActor.HostActorName);
        _hostActorPath = hostActor.Path;
    }
    #endregion

    #region [ Use ]
    public static ActorSystem Instance => RootSystem;

    public static void Send(object message)
    {
        if (_hostActorPath == null)
        {
            throw new Exception("Akka system has not being initialized.");
        }

        // send to host.
        RootSystem.ActorSelection(_hostActorPath).Tell(message);
    }
    #endregion
}
