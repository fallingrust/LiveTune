using Avalonia.Controls;
using Avalonia.Threading;
using LiveTune.Views.Pages;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            }
        }
    }



    public MainViewModel()
    {
        Queue<string> q = new Queue<string>();
        
        //  _currentPage = new HomePageViewModel();
        //Task.Run(async () =>
        //{
        //    await Task.Delay(4000);
        //    Dispatcher.UIThread.Invoke(() =>
        //    {
        //        CurrentPage = new SearchPageViewModel();
        //    });

        //});
    }
}
