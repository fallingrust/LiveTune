using LiveTune.Models;
using LiveTune.Utils;
using LiveTune.Views.Interfaces;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RadioBrowserSharp;

namespace LiveTune.ViewModels
{
    public class RecommandDetailPageViewModel : ViewModelBase, IStationList
    {
        private bool _isError;
        private bool _isLoading;
        public ObservableCollection<StationListItem> StationItemSource { get; set; } = [];

        public int PageSize => Consts.PAGE_SIZE;

        public int Offset { get; set; }
        public bool IsError { get => _isError; set => SetProperty(ref _isError, value); }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }
        public RecommandType RecommandType { get; set; }
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
                };
                if (RecommandType == RecommandType.RencentClick)
                {
                    paramsDic.Add("order", "clicktrend");
                }
                else if (RecommandType == RecommandType.TopVote)
                {
                    paramsDic.Add("order", "votes");
                }
                else if (RecommandType == RecommandType.TopClick)
                {
                    paramsDic.Add("order", "clickcount");
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
