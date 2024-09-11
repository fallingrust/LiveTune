using Avalonia.Controls;
using Avalonia.Interactivity;
using LiveTune.DataBase;
using LiveTune.Models;
using LiveTune.ViewModels;

namespace LiveTune.Views.Pages;

public partial class RecentPlayPage : UserControl
{
    public RecentPlayPage()
    {
        InitializeComponent();
        Loaded += OnRecentPlayPageLoaded;
    }

    private void OnRecentPlayPageLoaded(object? sender, RoutedEventArgs e)
    {
        if (DataContext is RecentPlayPageViewModel vm)
        {
            vm.StationItemSource.Clear();
            var rencentStations = DbCtx.GetRencentStations(0, 100);
            foreach (var station in rencentStations)
            {
                vm.StationItemSource.Add(StationListItem.Parse(station));
            }
        }
    }
}