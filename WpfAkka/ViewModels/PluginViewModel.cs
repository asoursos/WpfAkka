using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using WpfAkka.Models;

namespace WpfAkka.ViewModels;

internal partial class PluginViewModel : ObservableObject
{
    // https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/generators/observableproperty
    // http://dontcodetired.com/blog/post/AkkaNET-Dispatchers-and-User-Interface-Thread-Access

    public PluginViewModel(int id, string name, bool isAvailable = true)
    {
        Id = id;
        Name = name;
        IsAvailable = isAvailable;
        WaitingMessages = new ObservableCollection<EventMessageViewModel>();
        ProcessedMessages = new ObservableCollection<EventMessageViewModel>();
    }

    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string name;
    [ObservableProperty]
    private string description;

    [ObservableProperty]
    private bool isAvailable;

    [ObservableProperty]
    private int processedMessagesCount;

    public ObservableCollection<EventMessageViewModel> WaitingMessages { get; set; }
    public ObservableCollection<EventMessageViewModel> ProcessedMessages { get; set; }

    internal void OnMessageProcessed(PatientContextChanged message)
    {
        // check if its the ui thread or not
        App.Current.Dispatcher.Invoke(() =>
        {
            ProcessedMessages.Add(new EventMessageViewModel() { Message = $"m{message.Id}", Payload = message.Payload });
            ProcessedMessagesCount = ProcessedMessages.Count;
        });
    }
}


