using LiveTune.DataBase;
using LiveTune.Utils;
using LiveTune.ViewModels;
using RadioBrowserSharp.Models;

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
        private bool _isLike = false;
        public string StationName { get => _stationName; set => SetProperty(ref _stationName, value); }
        public string Language { get => _language; set => SetProperty(ref _language, value); }

        public int ClickCount { get => _clickCount; set => SetProperty(ref _clickCount, value); }
        public int VoteCount { get => _voteCount; set => SetProperty(ref _voteCount, value); }

        public string FaviconUrl { get => _faviconUrl; set => SetProperty(ref _faviconUrl, value); }

        public string Country { get => _country; set => SetProperty(ref _country, value); }
        public bool IsLike { get => _isLike; set => SetProperty(ref _isLike, value); }
        public string Url { get; set; } = string.Empty;
        public string StationId { get; set; } = string.Empty;

        public static StationListItem Parse(RencentStationEntity entity)
        {
            return new StationListItem()
            {
                ClickCount = entity.ClickCount,
                VoteCount = entity.VoteCount,
                StationId = entity.StationId,
                StationName = entity.StationName,
                Language = entity.Language,
                Url = entity.Url,
                Country = entity.Country,
                FaviconUrl = entity.FaviconUrl,
                IsLike = entity.IsLikeStation
            };
        }

        public static StationListItem Parse(LikeStationEntity entity)
        {
            return new StationListItem()
            {
                ClickCount = entity.ClickCount,
                VoteCount = entity.VoteCount,
                StationId = entity.StationId,
                StationName = entity.StationName,
                Language = entity.Language,
                Url = entity.Url,
                Country = entity.Country,
                FaviconUrl = entity.FaviconUrl,
                IsLike = entity.IsLikeStation
            };
        }

        public static StationListItem? Parse(RadioStation station)
        {
            if (string.IsNullOrWhiteSpace(station.Name)) return null;
            if (string.IsNullOrWhiteSpace(station.Url)) return null;
            if (string.IsNullOrWhiteSpace(station.StationUUID)) return null;
            return new StationListItem()
            {
                StationId = station.StationUUID,
                ClickCount = station.ClickCount,
                FaviconUrl = string.IsNullOrWhiteSpace(station.Favicon) || !station.Favicon.StartsWith("http") ? "avares://LiveTune/Assets/favicon.png" : station.Favicon,
                Language = string.IsNullOrWhiteSpace(station.Language) ? "-" : LocalUtil.GetLanguageisplayName(station.Language, station.Language),
                Country = string.IsNullOrWhiteSpace(station.CountryCode) ? "-" : LocalUtil.GetCountyDisplayName(station.CountryCode, station.CountryCode),
                StationName = station.Name.Trim(),
                VoteCount = station.Votes,
                IsLike = DbCtx.IsLikeStation(station.StationUUID),
                Url = station.Url,
            };
        }
    }
}
