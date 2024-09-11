using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using LiveTune.DataBase;
using LiveTune.ViewModels;
using LiveTune.Views;
using LiveTune.Views.Windows;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Tenray.ZoneTree;
using Tenray.ZoneTree.Comparers;
using Tenray.ZoneTree.Serializers;


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


        Task.Run(async () =>
        {
            using var zoneTree = new ZoneTreeFactory<int,RencentStationEntity>()
            .SetValueSerializer(new RencentStationEntitySerializer())
            
            .SetKeySerializer(new Int32Serializer()).OpenOrCreate();
            
            var iter = zoneTree.CreateIterator();
            while (iter.Next())
            {
                Console.WriteLine(iter.CurrentKey);
            }
        });
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
