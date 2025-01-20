using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WpfAkka.Models.Messages;

namespace WpfAkka.ViewModels;

internal partial class PluginViewModel : ObservableObject
{
    public PluginViewModel()
    {
        ProcessedMessages = new ObservableCollection<EventMessageViewModel>();
    }

    [ObservableProperty]
    protected int processedMessagesCount;

    public ObservableCollection<EventMessageViewModel> ProcessedMessages { get; set; }

    internal virtual void OnMessageProcessed(BaseEvent message)
    {
        // check if its the ui thread or not
        App.Current.Dispatcher.Invoke(() =>
        {
            ProcessedMessages.Add(new EventMessageViewModel() { Message = $"m{message.Id}", Payload = message.Payload });
            ProcessedMessagesCount = ProcessedMessages.Count;
        });
    }
}

internal partial class AresViewModel : PluginViewModel
{
    // https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/generators/observableproperty
    // http://dontcodetired.com/blog/post/AkkaNET-Dispatchers-and-User-Interface-Thread-Access

    
}
