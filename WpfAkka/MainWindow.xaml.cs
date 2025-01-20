using System.Collections.ObjectModel;
using System.Windows;
using WpfAkka.Actors;
using WpfAkka.ViewModels;

namespace WpfAkka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var plugins = new ObservableCollection<PluginViewModel>
            {
                new PluginViewModel(1, "Plugin 1"),
                new PluginViewModel(2, "Plugin 2"),
                new PluginViewModel(3, "Plugin 3"),
                new PluginViewModel(4, "Plugin 4")
            };

            // Initialize akka system
            AkkaSystem.Initialize(plugins);

            // create a dummy list of plugins
            var scenarios = new ObservableCollection<ScenarioViewModel>
            {
                new ScenarioViewModel(Scenario.ProviderOpensPatient),
                new ScenarioViewModel(Scenario.ProviderOpens10DifferentPatients),
                new ScenarioViewModel(Scenario.ProviderOpens100DifferentPatients)
            };

            // set the data context of the window to the list of plugins
            var selections = new Dictionary<int, string>() { { 0, "All" } };
            foreach (var item in plugins)
            {
                selections.Add(item.Id, item.Name);
            }

            DataContext = new MainWindowViewModel
            { 
                PluginSelections = selections, 
                Plugins = plugins, 
                Scenarios = scenarios 
            };
        }
    }
}