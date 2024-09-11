using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Input;

namespace LiveTune.ViewModels
{
    public partial class ApplicationViewModel : ViewModelBase
    {
        [RelayCommand]
        private static void ShowMainWindow()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime application && application.MainWindow != null)
            {
                application.MainWindow.WindowState = Avalonia.Controls.WindowState.Normal;
            }
        }

        [RelayCommand]
        private static void Quit()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime application)
            {
                application.Shutdown();
            }
        }
    }
}
