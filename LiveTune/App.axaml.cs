using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using LiveTune.ViewModels;
using LiveTune.Views;
using LiveTune.Views.Windows;
using System.Globalization;
using System.Runtime.InteropServices;


namespace LiveTune;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Assets.Resources.Culture = new CultureInfo("");
#if DEBUG
        AllocConsole();
#endif
        BindingPlugins.DataValidators.RemoveAt(0);       
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
            DataContext = new ApplicationViewModel();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
    [DllImport("kernel32.dll")]
    public static extern bool AllocConsole();


}
