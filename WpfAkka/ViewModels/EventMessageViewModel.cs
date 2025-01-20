using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfAkka.ViewModels;

internal partial class EventMessageViewModel: ObservableObject
{
    [ObservableProperty]
    private string? message;

    [ObservableProperty]
    private string? payload;

    [ObservableProperty]
    private bool isPopupOpen;

    [RelayCommand]
    public void TogglePopup()
    {
        IsPopupOpen = !isPopupOpen;
    }
}


