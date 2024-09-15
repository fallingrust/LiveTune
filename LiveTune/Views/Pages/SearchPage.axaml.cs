using Avalonia.Controls;
using Avalonia.Interactivity;
using LiveTune.Utils;
using LiveTune.ViewModels;
using RadioBrowserSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveTune.Views.Pages;

public partial class SearchPage : UserControl
{
    private int _offset = 0;
    private bool _loaded = false;
    private string _searchContent = string.Empty;
    public SearchPage(string searchContent)
    {
        InitializeComponent();
        Loaded += OnSearchPageLoaded;
        _searchContent = searchContent;
    }
    private async void OnStationListControlLoadMore(object? sender, RoutedEventArgs e)
    {
        await LoadAsync();
    }
    public async void UpdateSearchContent(string searchContent)
    {
        _searchContent = searchContent;
        await LoadAsync(true);
    }

    private async void OnSearchPageLoaded(object? sender, RoutedEventArgs e)
    {
        if (!_loaded)
        {
            _loaded = true;
            await LoadAsync(true);
        }
    }

    public async Task LoadAsync(bool firstLoad = false)
    {
        _offset = firstLoad ? 0 : _offset + Consts.PAGE_SIZE;
        var paramsDic = new Dictionary<string, string>
        {
            { "name", _searchContent },
            { "limit", Consts.PAGE_SIZE.ToString() },
            { "offset", $"{_offset}" },
            { "hidebroken", "true" },
            { "order", "clickcount" }
        };
        var radioStations = await RadioBrowserApi.SearchAsync(paramsDic);

        if (radioStations != null && DataContext is SearchPageViewModel vm)
        {
            vm.UpdateItemSource(radioStations, firstLoad);
        }
    }

}