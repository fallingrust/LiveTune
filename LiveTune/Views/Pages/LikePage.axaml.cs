using Avalonia.Controls;
using Avalonia.Interactivity;
using LiveTune.ViewModels;
using LiveTune.Views.Interfaces;
using System.Threading.Tasks;

namespace LiveTune.Views.Pages;

public partial class LikePage : UserControl, IPage
{
    private bool _loaded = false;
    public LikePage()
    {
        InitializeComponent();
        Loaded += OnLikePageLoaded;
    }
    private async void OnStationListControlLoadMore(object? sender, RoutedEventArgs e)
    {
        if (DataContext is LikePageViewModel vm)
        {
            await vm.LoadNextAsync();
        }
    }
    private async void OnStationListControlReload(object? sender, RoutedEventArgs e)
    {
        if (DataContext is LikePageViewModel vm)
        {
            await vm.ReloadAsync();
        }
    }
    public async Task RefreshAsync()
    {
        if (DataContext is LikePageViewModel vm)
        {
            await vm.LoadFirstAsync();
        }
    }

    private async void OnLikePageLoaded(object? sender, RoutedEventArgs e)
    {
        if (DataContext is LikePageViewModel vm)
        {
            await vm.LoadFirstAsync();
        }
    }
}