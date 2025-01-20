using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using WpfAkka.Actors;
using WpfAkka.Models;

namespace WpfAkka.ViewModels;

internal partial class MainWindowViewModel : ObservableObject
{
    public ObservableCollection<PluginViewModel> Plugins { get; internal set; } = new ObservableCollection<PluginViewModel>();
    public ObservableCollection<ScenarioViewModel> Scenarios { get; internal set; } = new ObservableCollection<ScenarioViewModel>();

    public KeyValuePair<int,string>? SelectedPlugin { get; set; }
    public ScenarioViewModel? SelectedScenario { get; set; }

    [ObservableProperty]
    private IDictionary<int, string> pluginSelections;

    public MainWindowViewModel()
    {
    }


    [RelayCommand]
    public void RunScenario()
    {
        if (SelectedPlugin == null)
        {
            MessageBox.Show("Please select a plugin", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (SelectedScenario == null)
        {
            MessageBox.Show("Please select a scenario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // Start the scenario
        Console.WriteLine("Scenario started");
        var messages = BuildMessages(SelectedScenario.Scenario);
        int? pluginId = SelectedPlugin.Value.Key == 0 ? null : SelectedPlugin.Value.Key;
        foreach (var item in messages)
        {
            AkkaSystem.Send(item, pluginId);
        }
    }

    private IEnumerable<PatientContextChanged> BuildMessages(Scenario scenario)
    {
        switch (scenario)
        {
            case Scenario.ProviderOpensPatient:
                yield return new PatientContextChanged();
                break;
            case Scenario.ProviderOpens10DifferentPatients:
                for(int i=0;i<10;i++)
                {
                    yield return new PatientContextChanged();
                }

                break;
            case Scenario.ProviderOpens100DifferentPatients:
                for (int i = 0; i < 100; i++)
                {
                    yield return new PatientContextChanged();
                }

                break;
            default:
                yield break;
        }
    }
}

internal class DesignMainWindowViewModel
{
    public ObservableCollection<PluginViewModel> Plugins { get; internal set; }
    public DesignMainWindowViewModel()
    {
        Plugins = new ObservableCollection<PluginViewModel>(new[]
        {
            new PluginViewModel(1, "Plugin 1", false)
            {
                ProcessedMessagesCount = 2,
                ProcessedMessages = new ObservableCollection<EventMessageViewModel>(new []
                                    {
                                        new EventMessageViewModel { Message = "m2" },
                                        new EventMessageViewModel { Message = "m1" }
                                    })
            },
            new PluginViewModel(2, "Plugin 2")
            {
                ProcessedMessagesCount = 1,
                ProcessedMessages = new ObservableCollection<EventMessageViewModel>(new []
                                    {
                                        new EventMessageViewModel { Message = "m2" }
                                    })
            }
        });
    }
}