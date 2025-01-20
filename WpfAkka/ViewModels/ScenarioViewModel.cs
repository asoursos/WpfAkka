using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfAkka.ViewModels;

public enum Scenario
{
    UserLoggingIn,
    ProviderOpensPatient,
    ProviderOpens10DifferentPatients,
    ProviderOpens100DifferentPatients,
}

internal partial class ScenarioViewModel : ObservableObject
{
    public ScenarioViewModel(Scenario scenario)
    {
        this.scenario = scenario;
    }

    [ObservableProperty]    
    private Scenario scenario;

    [RelayCommand]
    public void RunScenario()
    {
        // Start the scenario
        Console.WriteLine("Scenario started");
    }
}
