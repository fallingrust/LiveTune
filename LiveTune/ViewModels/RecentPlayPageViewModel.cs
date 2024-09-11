using LiveTune.Models;
using System.Collections.ObjectModel;

namespace LiveTune.ViewModels
{
    public class RecentPlayPageViewModel : ViewModelBase
    {
        public ObservableCollection<StationListItem> StationItemSource { get; set; } = [];
    }
}
