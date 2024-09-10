using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Shared;
using LiveTune.ViewModels;
using LiveTune.Views;
using LiveTune.Views.Windows;
using System;
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
        AllocConsole();
        Assets.Resources.Culture = new CultureInfo("zh");
       var _vlc = new LibVLC(true);
      var  _media = new Media(_vlc, new Uri("http://as-hls-ww-live.akamaized.net/pool_904/live/ww/bbc_radio_five_live/bbc_radio_five_live.isml/bbc_radio_five_live-audio%3d96000.norewind.m3u8"));
      var  _player = new MediaPlayer(_media);
        _player.Play();
        BindingPlugins.DataValidators.RemoveAt(0);       
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
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
