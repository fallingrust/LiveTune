using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using LiveTune.DataBase;
using LiveTune.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveTune.Views.Controls;

public partial class StationListControl : UserControl
{
    public static readonly StyledProperty<IEnumerable<StationListItem>> ItemSourceProperty = AvaloniaProperty.Register<StationListControl, IEnumerable<StationListItem>>(nameof(ItemSource));
    public static readonly StyledProperty<bool> IsLoadingProperty = AvaloniaProperty.Register<StationListControl, bool>(nameof(IsLoading));
    public static readonly StyledProperty<bool> IsErrorProperty = AvaloniaProperty.Register<StationListControl, bool>(nameof(IsError));

    public static readonly RoutedEvent<RoutedEventArgs> LoadMoreEvent = RoutedEvent.Register<StationListControl, RoutedEventArgs>(nameof(LoadMore), RoutingStrategies.Direct);
    public static readonly RoutedEvent<RoutedEventArgs> ReloadEvent = RoutedEvent.Register<StationListControl, RoutedEventArgs>(nameof(Reload), RoutingStrategies.Direct);
    public IEnumerable<StationListItem> ItemSource { get => GetValue(ItemSourceProperty); set => SetValue(ItemSourceProperty, value); }
    public bool IsLoading { get => GetValue(IsLoadingProperty); set => SetValue(IsLoadingProperty, value); }
    public bool IsError { get => GetValue(IsErrorProperty); set => SetValue(IsErrorProperty, value); }
    public event EventHandler<RoutedEventArgs> LoadMore{add => AddHandler(LoadMoreEvent, value);remove => RemoveHandler(LoadMoreEvent, value); }
    public event EventHandler<RoutedEventArgs> Reload { add => AddHandler(ReloadEvent, value); remove => RemoveHandler(ReloadEvent, value); }
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
            DbCtx.AddOrRemoveLikeStation(LikeStationEntity.Parse(item));
            WeakReferenceMessenger.Default.Send(new Messages.RadioLikeMessage(item));
        }
    }

    private void OnReloadButtonClick(object? sender, RoutedEventArgs e)
    {
        var args = new RoutedEventArgs(ReloadEvent);
        RaiseEvent(args);
    }
}