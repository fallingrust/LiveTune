using Avalonia.Threading;
using LiveTune.Models;
using RadioBrowserSharp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LiveTune.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        public ObservableCollection<StationListItem> StationItemSource { get; set; } = [];

        public void UpdateItemSource(IEnumerable<StationListItem> itemSources,bool clear = false)
        {
            if (!Dispatcher.UIThread.CheckAccess())
            {
                Dispatcher.UIThread.Invoke(() => UpdateItemSource(itemSources, clear));
                return;
            }
            if (clear) StationItemSource.Clear();
            foreach (var item in itemSources)
            {
                StationItemSource.Add(item);
            }
        }

        public void UpdateItemSource(IEnumerable<RadioStation> radioStations, bool clear = false)
        {
            if (!Dispatcher.UIThread.CheckAccess())
            {
                Dispatcher.UIThread.Invoke(() => UpdateItemSource(radioStations, clear));
                return;
            }
            if (clear) StationItemSource.Clear();
            foreach (var radioStation in radioStations)
            {
                var item = StationListItem.Parse(radioStation);
                if (item != null)
                    StationItemSource.Add(item);
            }
        }
    }
}
