using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using LiveTune.Views.Pages;
using System.Collections.Generic;

namespace LiveTune.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private ContentControl? _currentPage;

    private readonly Dictionary<string, ContentControl?> _baseViews = [];

    private ListBoxItem? _selectedMenuItem;
    private readonly Stack<ContentControl> _viewStacks = new();
    public ContentControl? CurrentPage{ get => _currentPage; set => SetProperty(ref _currentPage, value); }
    public ListBoxItem? SelectedMenuItem 
    {
        get => _selectedMenuItem;
        set
        {
            SetProperty(ref _selectedMenuItem, value);
            MenuSelectedHandler(value);
        }
    }

    public MainViewModel()
    {
        _baseViews.Add("recommand", new RecommandPage());
        _baseViews.Add("online", new OnlineStationPage());
        _baseViews.Add("recent", new RecentPlayPage());
        _baseViews.Add("like", new LikePage());
        WeakReferenceMessenger.Default.Register<MainViewModel, Messages.NavigateViewMessage>(this, OnNavigateViewMessageReceived);
    }
    private static void OnNavigateViewMessageReceived(MainViewModel vm, Messages.NavigateViewMessage message)
    {
        vm._viewStacks.Push(message.Value);
    }
    private void MenuSelectedHandler(ListBoxItem? item)
    {
        if (item == null)
        {
            CurrentPage = null;
        }
        else
        {
            var key = item.Tag?.ToString();
            if (string.IsNullOrWhiteSpace(key)) return;

            if(_baseViews.TryGetValue(key, out var currentPage))
            {
                CurrentPage = currentPage;
            }
        }
    }

    public void NavigateBack()
    {
        if (_viewStacks.TryPop(out var _))
        {
            if (_viewStacks.Count > 0)
            {
                CurrentPage = _viewStacks.Peek();
            }
            else
            {
                MenuSelectedHandler(SelectedMenuItem);
            }
        }
    }
}
