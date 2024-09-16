using Avalonia.Controls;
using Avalonia.Interactivity;
using LiveTune.ViewModels;

namespace LiveTune.Views.Pages;

public partial class SearchPage : UserControl
{
    private bool _loaded = false;
    public SearchPage(string searchContent)
    {
        InitializeComponent();
        Loaded += OnSearchPageLoaded;
        if (DataContext is SearchPageViewModel vm)
            vm.SearchContent = searchContent;
    }
    private async void OnStationListControlLoadMore(object? sender, RoutedEventArgs e)
    {
        if (DataContext is SearchPageViewModel vm)
            await vm.LoadNextAsync();
    }
    public async void UpdateSearchContent(string searchContent)
    {
        if (DataContext is SearchPageViewModel vm)
        {
            vm.SearchContent = searchContent;
            await vm.LoadFirstAsync();
        }
    }

    private async void OnSearchPageLoaded(object? sender, RoutedEventArgs e)
    {
        if (!_loaded)
        {
            _loaded = true;
            if (DataContext is SearchPageViewModel vm)
                await vm.LoadFirstAsync();
        }
    }
}