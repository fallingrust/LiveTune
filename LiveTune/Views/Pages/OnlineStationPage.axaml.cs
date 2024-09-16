using Avalonia.Controls;
using Avalonia.Interactivity;
using LiveTune.ViewModels;
using LiveTune.Views.Interfaces;
using System.Threading.Tasks;

namespace LiveTune.Views.Pages;

public partial class OnlineStationPage : UserControl, IPage
{
    private bool _loaded = false;
  
    public OnlineStationPage()
    {
        InitializeComponent();
        Loaded += OnlineStationPageLoaded;
    }

    private async void OnStationListControlLoadMore(object? sender, RoutedEventArgs e)
    {
        if (DataContext is OnlineStationPageViewModel vm)
        {
            await vm.LoadNextAsync();
        }
    }
    private async void OnStationListControlReload(object? sender, RoutedEventArgs e)
    {
        if (DataContext is OnlineStationPageViewModel vm)
        {
            await vm.ReloadAsync();
        }
    }

    private async void OnlineStationPageLoaded(object? sender, RoutedEventArgs e)
    {
        if (!_loaded)
        {
            _loaded = true;
            if (DataContext is OnlineStationPageViewModel vm)
            {
                await vm.LoadFirstAsync();
            }
        }
    }

    public async Task RefreshAsync()
    {
        if (DataContext is OnlineStationPageViewModel vm)
        {
            await vm.LoadFirstAsync();
        }
    }
}