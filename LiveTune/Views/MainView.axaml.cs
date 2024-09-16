using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using LiveTune.ViewModels;
using LiveTune.Views.Interfaces;
using LiveTune.Views.Pages;
using System;

namespace LiveTune.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        PART_TextBox_Search.KeyDown += OnSearchTextBoxKeyDown;
    }

    private void OnSearchTextBoxKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && sender is TextBox tb && !string.IsNullOrWhiteSpace(tb.Text) && DataContext is MainViewModel vm)
        {
            if (vm.CurrentPage is SearchPage searchPage)
            {
                searchPage.UpdateSearchContent(tb.Text);
            }
            else
            {
                var page = new SearchPage(tb.Text);
                WeakReferenceMessenger.Default.Send(new Messages.NavigateViewMessage(page));
            }
        }
    }

    private void OnTitleBarPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        (TopLevel.GetTopLevel(this) as Window)?.BeginMoveDrag(e);
    }

    private void OnCloseButtonClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    private void OnMinButtonClick(object? sender, RoutedEventArgs e)
    {
        if (TopLevel.GetTopLevel(this) is Window window)
        {
            window.WindowState = WindowState.Minimized;
        }
    }
    private void OnMaxButtonClick(object? sender, RoutedEventArgs e)
    {
        if (TopLevel.GetTopLevel(this) is Window window && sender is Button btn && btn.Content is PathIcon pathIcon)
        {
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
                if (this.TryFindResource("icon_max", out var resource) && resource is Avalonia.Media.StreamGeometry data) 
                {
                    pathIcon.Data = data;
                }
            }
            else if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
                if (this.TryFindResource("icon_normal", out var resource) && resource is Avalonia.Media.StreamGeometry data)
                {
                    pathIcon.Data = data;
                }
            }
        }
    }

    private async void OnRefreshButtonClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm && vm.CurrentPage is IPage page)
        {
            await page.RefreshAsync();
        }
    }

    private void OnNavigateBackButtonClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            vm.NavigateBack();
        }
    }
}
