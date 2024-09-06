using Avalonia.Controls;
using LiveTune.Views.Pages;
using System.Collections.Generic;

namespace LiveTune.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    private ContentControl? _currentPage;

    private readonly List<ViewModelBase> _views = [];

    private ListBoxItem? _selectedMenuItem;
    public ContentControl? CurrentPage{ get => _currentPage; set => SetProperty(ref _currentPage, value); }
    public ListBoxItem? SelectedMenuItem 
    {
        get => _selectedMenuItem;
        set
        {
            SetProperty(ref _selectedMenuItem, value);

            if (value == null)
            {
                CurrentPage = null;
            }
            else
            {
                if (value.Tag?.ToString() == "recommand")
                {
                    CurrentPage = new RecommandPage();
                }
                else if (value.Tag?.ToString() == "online")
                {
                    CurrentPage = new OnlineStationPage();
                }
                else if (value.Tag?.ToString() == "recent")
                {
                    CurrentPage = new RecentPlayPage();
                }
                else if (value.Tag?.ToString() == "like")
                {
                    CurrentPage = new LikePage();
                }
            }
        }
    }



    public MainViewModel()
    {
        
    }
}
