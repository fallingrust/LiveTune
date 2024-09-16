using LiveTune.Models;
using System.Collections.ObjectModel;
using LiveTune.Views.Interfaces;
using System.Threading.Tasks;
using LiveTune.Utils;
using RadioBrowserSharp.Models;
using RadioBrowserSharp;
using System;
using System.Collections.Generic;
using System.Resources;

namespace LiveTune.ViewModels
{
    public class OnlineStationPageViewModel : ViewModelBase, IStationList
    {
        private RadioItem? _selecteLanguage;
        private RadioItem? _selecteCountry;
        private RadioItem? _selecteTag;
        private bool _isError;
        private bool _isLoading;
        public ObservableCollection<StationListItem> StationItemSource { get; set; } = [];
        public ObservableCollection<RadioItem> Languages { get; set; } = [];
        public ObservableCollection<RadioItem> Countries { get; set; } = [];
        public ObservableCollection<RadioItem> Tags { get; set; } = [];
        
        public RadioItem? SelecteLanguage
        {
            get => _selecteLanguage;
            set
            {
                SetProperty(ref _selecteLanguage, value);
                _ = LoadFirstAsync();
            }
        }
        public RadioItem? SelecteCountry
        {
            get => _selecteCountry;
            set
            {
                SetProperty(ref _selecteCountry, value);
                 _ = LoadFirstAsync();
            }
        }
        public RadioItem? SelecteTag
        {
            get => _selecteTag;
            set
            {
                SetProperty(ref _selecteTag, value);
                _ = LoadFirstAsync();
            }
        }

        public int Offset { get; set; }
        public int PageSize { get => Consts.PAGE_SIZE; }
        public bool IsError { get => _isError; set => SetProperty(ref _isError, value); }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }



        public OnlineStationPageViewModel()
        {
            LoadSearchMenus();
        }

        private  void LoadSearchMenus()
        {
            Countries.Add(new RadioItem(Assets.Resources.All, "All", true));
            Countries.Add(new RadioItem(Assets.Resources.US, "US"));
            Countries.Add(new RadioItem(Assets.Resources.DE, "DE"));
            Countries.Add(new RadioItem(Assets.Resources.RU, "RU"));
            Countries.Add(new RadioItem(Assets.Resources.FR, "FR"));
            Countries.Add(new RadioItem(Assets.Resources.GR, "GR"));
            Countries.Add(new RadioItem(Assets.Resources.CN, "CN"));
            Countries.Add(new RadioItem(Assets.Resources.GB, "GB"));
            Countries.Add(new RadioItem(Assets.Resources.AU, "AU"));



            Languages.Add(new RadioItem("全部", "All", true));
            Languages.Add(new RadioItem(Assets.Resources.english, "english"));
            Languages.Add(new RadioItem(Assets.Resources.spanish, "spanish"));
            Languages.Add(new RadioItem(Assets.Resources.german, "german"));
            Languages.Add(new RadioItem(Assets.Resources.french, "french"));
            Languages.Add(new RadioItem(Assets.Resources.chinese, "chinese"));
            Languages.Add(new RadioItem(Assets.Resources.italian, "italian"));
            Languages.Add(new RadioItem(Assets.Resources.russian, "russian"));
            Languages.Add(new RadioItem(Assets.Resources.greek, "greek"));


            Tags.Add(new RadioItem("全部", "All", true));
            Tags.Add(new RadioItem(Assets.Resources.pop, "pop"));
            Tags.Add(new RadioItem(Assets.Resources.music, "music"));
            Tags.Add(new RadioItem(Assets.Resources.news, "news"));
            Tags.Add(new RadioItem(Assets.Resources.rock, "rock"));
            Tags.Add(new RadioItem(Assets.Resources.classical, "classical"));
            Tags.Add(new RadioItem(Assets.Resources.talk, "talk"));
        }

        public async Task LoadFirstAsync()
        {
            if (IsLoading) return;

            Offset = 0;
            StationItemSource.Clear();
            await DoLoadAsync();
        }

        public async Task LoadNextAsync()
        {
            if (IsLoading) return;

            Offset += PageSize;
            await DoLoadAsync();
        }

        public async Task ReloadAsync()
        {
            await LoadFirstAsync();
        }

        private async Task DoLoadAsync()
        {
            IsError = false;
            IsLoading = true;
            try
            {
                var paramsDic = new Dictionary<string, string>
                {
                    { "limit", $"{PageSize}" },
                    { "offset", $"{Offset}" },
                    { "hidebroken", "true" },
                    { "reverse", "true" },
                    { "order", "clickcount" }
                };
                if (SelecteLanguage != null && SelecteLanguage.Tag != "All")
                {
                    paramsDic.Add("language", SelecteLanguage.Tag);
                }

                if (SelecteCountry != null && SelecteCountry.Tag != "All")
                {
                    paramsDic.Add("countrycode", SelecteCountry.Tag);
                }
                if (SelecteTag != null && SelecteTag.Tag != "All")
                {
                    paramsDic.Add("tag", SelecteTag.Tag);
                }
                var stations = await RadioBrowserApi.SearchAsync(paramsDic);

                if (stations != null)
                {
                    foreach (var station in stations)
                    {
                        if (string.IsNullOrWhiteSpace(station.Name)) continue;
                        if (string.IsNullOrWhiteSpace(station.Url)) continue;
                        if (string.IsNullOrWhiteSpace(station.StationUUID)) continue;
                        var item = StationListItem.Parse(station);
                        if (item != null)
                            StationItemSource.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                IsError = true;
                Console.WriteLine(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
