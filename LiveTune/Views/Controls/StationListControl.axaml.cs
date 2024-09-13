using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using LiveTune.Models;
using System;
using System.Collections.Generic;

namespace LiveTune.Views.Controls;

public partial class StationListControl : UserControl
{
    public static readonly StyledProperty<IEnumerable<StationListItem>> ItemSourceProperty = AvaloniaProperty.Register<StationListControl, IEnumerable<StationListItem>>(nameof(ItemSource));

    public IEnumerable<StationListItem> ItemSource { get => GetValue(ItemSourceProperty); set => SetValue(ItemSourceProperty, value); }
    public static readonly RoutedEvent<RoutedEventArgs> LoadMoreEvent = RoutedEvent.Register<StationListControl, RoutedEventArgs>(nameof(LoadMore), RoutingStrategies.Direct);

    public event EventHandler<RoutedEventArgs> LoadMore{add => AddHandler(LoadMoreEvent, value);remove => RemoveHandler(LoadMoreEvent, value); }

    public StationListControl()
    {
        InitializeComponent();       
        PART_ListBox_StationList.PointerWheelChanged += OnStationListPointerWheelChanged;
    }

    private void OnStationListPointerWheelChanged(object? sender, Avalonia.Input.PointerWheelEventArgs e)
    {      
        if (e.Delta.Y < 0)
        {
            var args = new RoutedEventArgs(LoadMoreEvent);
            RaiseEvent(args);
        }
    }

    private void OnPlayButtonClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext is StationListItem item)
        {
            WeakReferenceMessenger.Default.Send(new Messages.RadioPlayMessage(item));
        }
    }

    private void OnLikeButtonClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext is StationListItem item)
        {
            item.IsLike = !item.IsLike;
            WeakReferenceMessenger.Default.Send(new Messages.RadioLikeMessage(item));
        }
    }

    //private void OnStationListLoaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    //{
    //    _scrollViewer = PART_ListBox_StationList.GetTemplateChildren().FirstOrDefault(p => p.Name == "PART_ScrollViewer") as ScrollViewer;

    //    if (_scrollViewer != null)
    //    {
    //        _scrollViewer.ScrollChanged += OnScrollChanged;
    //    }
    //}

    //private void OnScrollChanged(object? sender, ScrollChangedEventArgs e)
    //{
    //    Debug.WriteLine($"{e.OffsetDelta.X}  {e.OffsetDelta.Y}   {e.ViewportDelta.X}  {e.ViewportDelta.Y} {_scrollViewer.Offset.Y}    {_scrollViewer.Viewport.Height} {_scrollViewer.Extent.Height}");
    //}
}