using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace LiveTune.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void OnTitleBarPointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
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
}
