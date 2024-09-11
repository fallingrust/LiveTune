using LiveTune.ViewModels;

namespace LiveTune.Models
{
    public class StationListItem : ViewModelBase
    {
        private string _stationName = string.Empty;
        private string _language = string.Empty;
        private int _clickCount;
        private int _voteCount;
        private string _faviconUrl = string.Empty;
        private string _country = string.Empty;
        public string StationName { get => _stationName; set => SetProperty(ref _stationName, value); }
        public string Language { get => _language; set => SetProperty(ref _language, value); }

        public int ClickCount { get => _clickCount; set => SetProperty(ref _clickCount, value); }
        public int VoteCount { get => _voteCount; set => SetProperty(ref _voteCount, value); }

        public string FaviconUrl { get => _faviconUrl; set => SetProperty(ref _faviconUrl, value); }

        public string Country { get => _country; set => SetProperty(ref _country, value); }
        public string Url { get; set; } = string.Empty;
    }
}
