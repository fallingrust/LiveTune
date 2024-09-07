using LiveTune.Models;
using RadioBrowserSharp.Models;
using RadioBrowserSharp;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LiveTune.ViewModels
{
    public class OnlineStationPageViewModel : ViewModelBase
    {
        private RadioItem? _selecteLanguage;
        private RadioItem? _selecteCountry;
        private RadioItem? _selecteTag;
        private bool _loading = false;
        public ObservableCollection<StationListItem> StationItemSource { get; set; } = [];
        public ObservableCollection<RadioItem> Languages { get; set; } = [];
        public ObservableCollection<RadioItem> Countries { get; set; } = [];
        public ObservableCollection<RadioItem> Tags { get; set; } = [];
        public bool Loading { get => _loading; set => SetProperty(ref _loading, value); }
        public RadioItem? SelecteLanguage
        {
            get => _selecteLanguage;
            set
            {
                SetProperty(ref _selecteLanguage, value);
            }
        }
        public RadioItem? SelecteCountry
        {
            get => _selecteCountry;
            set
            {
                SetProperty(ref _selecteCountry, value);
            }
        }
        public RadioItem? SelecteTag
        {
            get => _selecteTag;
            set
            {
                SetProperty(ref _selecteTag, value);
            }
        }
        private const int LIMIT = 30;
        private int _offset = 0;
      
        public OnlineStationPageViewModel()
        {
            LoadSearchMenus();
        }

        private void LoadSearchMenus()
        {
            Languages.Add(new RadioItem("全部", true));
            Languages.Add(new RadioItem("中文"));
            Languages.Add(new RadioItem("英语"));

            Countries.Add(new RadioItem("全部", true));
            Countries.Add(new RadioItem("中国"));
            Countries.Add(new RadioItem("美国"));
            Countries.Add(new RadioItem("英国"));

            Tags.Add(new RadioItem("全部", true));
            Tags.Add(new RadioItem("音乐"));
            Tags.Add(new RadioItem("新闻"));
        }

        public async Task FirstLoadAsync()
        {
            Loading = true;
            _offset = 0;
            var param = new ListStationsParams()
            {
                HideBroken = true,
                Limit = LIMIT,
                Offset = (uint)_offset,
                OrderType = OrderType.Name,
            };
            var stations = await RadioBrowserApi.ListAllRadioStationsAsync(param);

            if (stations != null)
            {
                StationItemSource.Clear();

                foreach (var station in stations)
                {
                    if (string.IsNullOrWhiteSpace(station.Name)) continue;
                    var item = new StationListItem()
                    {
                        ClickCount = station.ClickCount,
                        FaviconUrl = station.Favicon,
                        Language = station.Language,
                        StationName = station.Name.Trim(),
                        VoteCount = station.Votes,
                    };
                   StationItemSource.Add(item);
                }
            }
            Loading = false;
        }

        public async Task LoadNextPageAsync()
        {
            Loading = true;
            _offset += LIMIT;
            var param = new ListStationsParams()
            {
                HideBroken = true,
                Limit = LIMIT,
                Offset = (uint)_offset,
                OrderType = OrderType.Name,
            };
            var stations = await RadioBrowserApi.ListAllRadioStationsAsync(param);

            if (stations != null)
            {
                foreach (var station in stations)
                {
                    if (string.IsNullOrWhiteSpace(station.Name)) continue;
                    var item = new StationListItem()
                    {
                        ClickCount = station.ClickCount,
                        FaviconUrl = station.Favicon,
                        Language = station.Language,
                        StationName = station.Name.Trim(),
                        VoteCount = station.Votes,
                    };
                    StationItemSource.Add(item);
                }
            }
            Loading = false;
        }
    }
}
