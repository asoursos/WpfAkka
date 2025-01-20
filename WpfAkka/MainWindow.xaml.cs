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

            var hostViewModel = new HostViewModel();

            // Initialize akka system
            AkkaSystem.Initialize(hostViewModel);

            DataContext = hostViewModel;
        }
    }
}