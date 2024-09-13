using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using LiveTune.ViewModels;

namespace LiveTune;

public partial class PlayCardControl : UserControl
{
    public PlayCardControl()
    {
        InitializeComponent();
    }

    private void OnPlayToggleButtonClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is PlayCardViewModel vm)
        {
            if (vm.IsPlaying)
            {
                vm.RadioPlayer?.Pause();
            }
            else
            {
                vm?.RadioPlayer?.Play();
            }
        }
    }

    private void OnLikeToggleButtonClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is PlayCardViewModel vm && vm.PlayStation != null)
        {
            vm.IsLikeStation = !vm.IsLikeStation;
            vm.PlayStation.IsLike = vm.IsLikeStation;
            WeakReferenceMessenger.Default.Send(new Messages.RadioLikeMessage(vm.PlayStation));
        }
    }
}