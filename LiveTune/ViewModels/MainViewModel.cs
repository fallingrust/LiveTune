using Avalonia.Threading;
using System.Threading.Tasks;

namespace LiveTune.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    private ViewModelBase _currentPage;

    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set => SetProperty(ref _currentPage, value);
    }

    public MainViewModel()
    {
          _currentPage = new HomePageViewModel();
        Task.Run(async () =>
        {
            await Task.Delay(4000);
            Dispatcher.UIThread.Invoke(() =>
            {
                CurrentPage = new SearchPageViewModel();
            });

        });
    }
}
