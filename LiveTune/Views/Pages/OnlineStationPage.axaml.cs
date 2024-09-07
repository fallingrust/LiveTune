using Avalonia.Controls;
using LiveTune.ViewModels;

namespace LiveTune.Views.Pages;

public partial class OnlineStationPage : UserControl
{
    private bool _loaded = false;
  
    public OnlineStationPage()
    {
        InitializeComponent();
        Loaded += OnlineStationPageLoaded;
    }

    private async void OnStationListControlLoadMore(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is OnlineStationPageViewModel vm)
        {
            await vm.LoadNextPageAsync();
        }
    }

    private async void OnlineStationPageLoaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is OnlineStationPageViewModel vm)
        {
            await vm.FirstLoadAsync();
        }
    }
}