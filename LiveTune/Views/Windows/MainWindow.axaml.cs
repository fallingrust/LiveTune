using Avalonia.Controls;
using System.Threading.Tasks;
using System;
using LibVLCSharp.Shared;


namespace LiveTune.Views.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ExtendClientAreaToDecorationsHint = true;
        ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.NoChrome;
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (!Design.IsDesignMode)
        {
           
           

            LibVLCSharp.Shared.LibVLC vlc = new LibVLCSharp.Shared.LibVLC();
            var media = new LibVLCSharp.Shared.Media(vlc, new Uri(@"http://playtv-live.ifeng.com/live/06OLEGEGM4G_audio.m3u8"));
            var mediaplayer = new MediaPlayer(media);

            mediaplayer.Play();

        }
    }
    void DownloadProcedure(IntPtr Buffer, int Length, IntPtr User)
    {

    }
}
