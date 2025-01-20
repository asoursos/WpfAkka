using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using WpfAkka.Actors;
using WpfAkka.Models.Messages;

namespace WpfAkka.ViewModels;

internal partial class HostViewModel : ObservableObject
{
    public ObservableCollection<AresViewModel> Plugins { get; internal set; } = new ObservableCollection<AresViewModel>();
    public ObservableCollection<ScenarioViewModel> Scenarios { get; internal set; } = new ObservableCollection<ScenarioViewModel>();

    public KeyValuePair<int,string>? SelectedPlugin { get; set; }
    public ScenarioViewModel? SelectedScenario { get; set; }

    [ObservableProperty]
    private AuthViewModel authViewModel;

    [ObservableProperty]
    private AresViewModel aresViewModel;

    [ObservableProperty]
    private ToastViewModel toastViewModel;

    [ObservableProperty]
    private bool isMainWindowOpened;

    public HostViewModel()
    {
        Scenarios = new ObservableCollection<ScenarioViewModel>
            {
                new ScenarioViewModel(Scenario.UserLoggingIn),
                new ScenarioViewModel(Scenario.UserLoggingOut),
                new ScenarioViewModel(Scenario.ProviderOpensPatient),
                new ScenarioViewModel(Scenario.ProviderOpens10DifferentPatients),
                new ScenarioViewModel(Scenario.ProviderOpens100DifferentPatients)
            };

        aresViewModel = new AresViewModel();
        authViewModel = new AuthViewModel();
        toastViewModel = new ToastViewModel();
        IsMainWindowOpened = true;
    }


    [RelayCommand]
    public void RunScenario()
    {
        if (SelectedScenario == null)
        {
            MessageBox.Show("Please select a scenario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // Start the scenario
        Console.WriteLine("Scenario started");
        var messages = BuildMessages(SelectedScenario.Scenario);

        foreach (var item in messages)
        {
            AkkaSystem.Send(item);
        }
    }

    private IEnumerable<object> BuildMessages(Scenario scenario)
    {
        switch (scenario)
        {
            case Scenario.UserLoggingIn:
                yield return new UserLoggingIn();
                break;
            case Scenario.UserLoggingOut:
                yield return new UserLoggingOut();
                break;
            case Scenario.ProviderOpensPatient:
                yield return new PatientOpenedInEMR();
                break;
            case Scenario.ProviderOpens10DifferentPatients:
                for(int i=0;i<10;i++)
                {
                    yield return new PatientOpenedInEMR();
                }

                break;
            case Scenario.ProviderOpens100DifferentPatients:
                for (int i = 0; i < 100; i++)
                {
                    yield return new PatientOpenedInEMR();
                }

                break;
            default:
                yield break;
        }
    }

    internal void OnPatientOpened(PatientOpenedInEMR msg)
    {
        if (IsMainWindowOpened)
        ToastViewModel.OnMessageProcessed(msg);
    }
}

internal partial class DesignMainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private AuthViewModel authViewModel;

    [ObservableProperty]
    private AresViewModel aresViewModel;

    [ObservableProperty]
    private ToastViewModel toastViewModel;

    public DesignMainWindowViewModel()
    {
        this.authViewModel = new AuthViewModel()
        {
            ProcessedMessagesCount = 2,
            ProcessedMessages = new ObservableCollection<EventMessageViewModel>(new[]
                                    {
                                        new EventMessageViewModel { Message = "m1" }
                                    })
        };

        this.aresViewModel = new AresViewModel()
        {
            ProcessedMessagesCount = 2,
            ProcessedMessages = new ObservableCollection<EventMessageViewModel>(new[]
                                    {
                                        new EventMessageViewModel { Message = "m3" }
                                    })
        };

        this.toastViewModel = new ToastViewModel()
        {
            ProcessedMessagesCount = 1,
            ProcessedMessages = new ObservableCollection<EventMessageViewModel>(new[]
                                    {
                                        new EventMessageViewModel { Message = "m2" }
                                    })
        };
    }
}