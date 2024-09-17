using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
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
    public class LikePageViewModel : ViewModelBase, IStationList
    {
        private bool _isError;
        private bool _isLoading;
        public ObservableCollection<StationListItem> StationItemSource { get; set; } = [];

        public int PageSize => Consts.PAGE_SIZE;

        public int Offset { get; set; }
        public bool IsError { get => _isError; set => SetProperty(ref _isError, value); }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }

        public LikePageViewModel()
        {
            WeakReferenceMessenger.Default.Register<LikePageViewModel, Messages.RadioLikeMessage>(this, OnRadioLikeMessageReceived);
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
                var likeStations = await DbCtx.GetLikeStationsAsync(Offset, PageSize);
                foreach (var station in likeStations)
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

        private static void OnRadioLikeMessageReceived(LikePageViewModel vm, Messages.RadioLikeMessage message)
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                var station = vm.StationItemSource.FirstOrDefault(p => p.StationId == message.Value.StationId);
                if (!message.Value.IsLike && station != null)
                {
                    vm.StationItemSource.Remove(station);
                }
            });
        }
    }
}
