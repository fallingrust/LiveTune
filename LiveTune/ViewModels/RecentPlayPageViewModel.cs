using LiveTune.DataBase;
using LiveTune.Models;
using LiveTune.Utils;
using LiveTune.Views.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LiveTune.ViewModels
{
    public class RecentPlayPageViewModel : ViewModelBase, IStationList
    {
        private bool _isError;
        private bool _isLoading;
        public ObservableCollection<StationListItem> StationItemSource { get; set; } = [];

        public int PageSize => Consts.PAGE_SIZE;

        public int Offset { get; set; }
        public bool IsError { get => _isError; set => SetProperty(ref _isError, value); }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }


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
                var stations = await DbCtx.GetRencentStationsAsync(Offset, PageSize);
                foreach (var station in stations)
                {
                    if (!StationItemSource.Any(p => p.StationId == station.StationId))
                        StationItemSource.Add(StationListItem.Parse(station));
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
