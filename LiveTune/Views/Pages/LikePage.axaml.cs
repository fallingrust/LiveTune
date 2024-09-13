using Avalonia.Controls;
using LiveTune.DataBase;
using LiveTune.Models;
using LiveTune.ViewModels;

namespace LiveTune.Views.Pages;

public partial class LikePage : UserControl
{
    public LikePage()
    {
        InitializeComponent();
        Loaded += OnLikePageLoaded;
    }

    private void OnLikePageLoaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        FirstLoad();
    }


    private void FirstLoad()
    {
        if (DataContext is LikePageViewModel vm)
        {
            vm.StationItemSource.Clear();
            var likeStations = DbCtx.GetLikeStations(0, 100);
            foreach (var station in likeStations)
            {
                vm.StationItemSource.Add(StationListItem.Parse(station));
            }
        }
    }

    private void LoadNextPage()
    {

    }
}