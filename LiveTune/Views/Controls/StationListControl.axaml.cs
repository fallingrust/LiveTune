using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using LiveTune.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveTune.Views.Controls;

public partial class StationListControl : UserControl
{
    public static readonly StyledProperty<IEnumerable<StationListItem>> ItemSourceProperty = AvaloniaProperty.Register<StationListControl, IEnumerable<StationListItem>>(nameof(ItemSource));
    public static readonly StyledProperty<bool> IsLoadingProperty = AvaloniaProperty.Register<StationListControl, bool>(nameof(IsLoading));
    public static readonly RoutedEvent<RoutedEventArgs> LoadMoreEvent = RoutedEvent.Register<StationListControl, RoutedEventArgs>(nameof(LoadMore), RoutingStrategies.Direct);
    public IEnumerable<StationListItem> ItemSource { get => GetValue(ItemSourceProperty); set => SetValue(ItemSourceProperty, value); }
    public bool IsLoading { get => GetValue(IsLoadingProperty); set => SetValue(IsLoadingProperty, value); }

    public event EventHandler<RoutedEventArgs> LoadMore{add => AddHandler(LoadMoreEvent, value);remove => RemoveHandler(LoadMoreEvent, value); }

    public StationListControl()
    {       
        InitializeComponent();       
        PART_ListBox_StationList.PointerWheelChanged += OnStationListPointerWheelChanged;
        WeakReferenceMessenger.Default.Register<StationListControl, Messages.RadioLikeMessage>(this, OnRadioLikeMessageReceived);
    }
    ~StationListControl()
    {
        WeakReferenceMessenger.Default.Unregister<StationListControl>(this);
    }
    private static void OnRadioLikeMessageReceived(StationListControl view, Messages.RadioLikeMessage message)
    {
        if (view.ItemSource == null || !view.ItemSource.Any()) return;
        Dispatcher.UIThread.Post(() =>
        {
            var item = view.ItemSource.FirstOrDefault(p => p.StationId == message.Value.StationId);
            if (item != null)
            {
                item.IsLike = message.Value.IsLike;
            }
        });
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