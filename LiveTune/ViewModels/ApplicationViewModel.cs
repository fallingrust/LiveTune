using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;

namespace LiveTune.ViewModels
{
    public partial class ApplicationViewModel : ViewModelBase
    {
        [RelayCommand]
        private static void ShowMainWindow()
        {
            Dispatcher.UIThread.Post(() =>
            {
                if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime application && application.MainWindow != null)
                {
                    if (application.MainWindow.WindowState == Avalonia.Controls.WindowState.Minimized)
                        application.MainWindow.WindowState = Avalonia.Controls.WindowState.Normal;
                    if (!application.MainWindow.ShowInTaskbar)
                        application.MainWindow.ShowInTaskbar = true;
                    application.MainWindow.Activate();
                }
            });
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
