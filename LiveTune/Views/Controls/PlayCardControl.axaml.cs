using Avalonia.Controls;
using Avalonia.Interactivity;
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
}