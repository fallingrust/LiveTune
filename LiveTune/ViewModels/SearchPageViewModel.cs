using Avalonia.Threading;
using LiveTune.Models;
using LiveTune.Utils;
using LiveTune.Views.Interfaces;
using RadioBrowserSharp;
using RadioBrowserSharp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LiveTune.ViewModels
{
    public class SearchPageViewModel : ViewModelBase, IStationList
    {
        private bool _isError;
        private bool _isLoading;
        public ObservableCollection<StationListItem> StationItemSource { get; set; } = [];

        public int PageSize => Consts.PAGE_SIZE;

        public int Offset { get; set; }
        public bool IsError { get => _isError; set => SetProperty(ref _isError, value); }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }

        public string SearchContent { get; set; } = string.Empty;
        public SearchPageViewModel()
        {
            
        }
        public async Task LoadFirstAsync()
        {
            if (IsLoading) return;
            IsError = false; 
            IsLoading = true;
            Offset = 0;
            var paramsDic = new Dictionary<string, string>
            {
                { "name", SearchContent },
                { "limit", $"{PageSize}" },
                { "offset", $"{Offset}" },
                { "hidebroken", "true" },
                { "reverse", "true" },
                { "order", "clickcount" }
            };
            try
            {
                var radioStations = await RadioBrowserApi.SearchAsync(paramsDic);

                if (radioStations != null)
                {
                    UpdateItemSource(radioStations, true);
                }
            }
            catch(Exception e)
            {
                IsError = true;
                Console.WriteLine(e.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task LoadNextAsync()
        {
            if (IsLoading) return;
            IsError = false;
            IsLoading = true;
            Offset += PageSize;
            var paramsDic = new Dictionary<string, string>
            {
                { "name", SearchContent },
                { "limit", $"{PageSize}" },
                { "offset", $"{Offset}" },
                { "hidebroken", "true" },
                { "order", "clickcount" }
            };
            try
            {
                var radioStations = await RadioBrowserApi.SearchAsync(paramsDic);

                if (radioStations != null)
                {
                    UpdateItemSource(radioStations, false);
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

        public async Task ReloadAsync()
        {
            await LoadFirstAsync();
        }

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
