using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
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

   
    //private  ScrollViewer? _scrollViewer;
    public StationListControl()
    {
        InitializeComponent();
        //ItemSource = new ObservableCollection<StationListItem>()
        //{
        //    new StationListItem(){ StationName = "1111",  Language="中文", ClickCount= 100, VoteCount = 58,  FaviconUrl="http://m.ajmide.com/favicon.ico" },
        //    new StationListItem(){ StationName = "1111", Language="中文", ClickCount= 100, VoteCount = 58 , FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},

        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //    new StationListItem(){ StationName = "1112221", Language="中文", ClickCount= 100, VoteCount = 58, FaviconUrl="http://m.ajmide.com/favicon.ico"},
        //};
        //PART_ListBox_StationList.Loaded += OnStationListLoaded;
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