using Avalonia.Controls;
using Avalonia.Interactivity;
using LiveTune.Models;
using LiveTune.ViewModels;
using LiveTune.Views.Interfaces;
using System.Threading.Tasks;

namespace LiveTune.Views.Pages;

public partial class RecommandDetailPage : UserControl, IPage
{
    private bool _loaded = false;
    public RecommandDetailPage()
    {
        InitializeComponent();
        Loaded += OnRecommandDetailPageLoaded;
    }
    public RecommandDetailPage(RecommandType recommandType) : this()
    {
        if (DataContext is RecommandDetailPageViewModel vm)
            vm.RecommandType = recommandType;
    }

    private async void OnRecommandDetailPageLoaded(object? sender, RoutedEventArgs e)
    {
        if (!_loaded)
        {
            _loaded = true;
            if (DataContext is RecommandDetailPageViewModel vm)
            {
               await vm.LoadFirstAsync();
            }
        }
    }
    private async void OnStationListControlLoadMore(object? sender, RoutedEventArgs e)
    {
        if (DataContext is RecommandDetailPageViewModel vm)
        {
            await vm.LoadNextAsync();
        }
    }
    private async void OnStationListControlReload(object? sender, RoutedEventArgs e)
    {
        if (DataContext is RecommandDetailPageViewModel vm)
        {
            await vm.ReloadAsync();
        }
    }

    private async void OnlineStationPageLoaded(object? sender, RoutedEventArgs e)
    {
        if (!_loaded)
        {
            _loaded = true;
            if (DataContext is RecommandDetailPageViewModel vm)
            {
                await vm.LoadFirstAsync();
            }
        }
    }

    public async Task RefreshAsync()
    {
        if (DataContext is RecommandDetailPageViewModel vm)
        {
            await vm.LoadFirstAsync();
        }
    }
}