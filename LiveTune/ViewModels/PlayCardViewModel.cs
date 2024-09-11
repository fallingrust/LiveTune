using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using LiveTune.DataBase;
using LiveTune.Player;
using System.Diagnostics;

namespace LiveTune.ViewModels
{
    public class PlayCardViewModel : ViewModelBase
    {
        private bool _isPlaying;
        private string _title = string.Empty;
        private long _playTime = 0;
        private string _faviconUrl = string.Empty;
        private string _url = string.Empty;
        private bool _buffering = false;
        
        public string FaviconUrl { get => _faviconUrl; set => SetProperty(ref _faviconUrl, value); }
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public bool IsPlaying { get => _isPlaying; set => SetProperty(ref _isPlaying, value); }
        public long PlayTime { get => _playTime; set => SetProperty(ref _playTime, value); }

        public bool Buffering { get => _buffering; set => SetProperty(ref _buffering, value); }
        

        public string Url
        {
            get => _url;
            set
            {
                if (_url != value)
                {
                    _url = value;
                    CreatePlayer(_url);
                }
            }
        }
        
        public IRadioPlayer? RadioPlayer { get; private set; }
        
        public PlayCardViewModel()
        {
            WeakReferenceMessenger.Default.Register<PlayCardViewModel, Messages.RadioPlayMessage>(this, OnRadioPlayMessageReceived);
        }

        private void CreatePlayer(string url)
        {
            ReleasePlayer();
            RadioPlayer ??= new VlcRadioPlayer(url);
            RadioPlayer.StatusChanged += OnRadioPlayerStatusChanged;
            RadioPlayer.TimeChanged += OnRadioPlayerTimeChanged;
            RadioPlayer.BufferingChanged += OnRadioPlayerBufferingChanged;
            RadioPlayer.Play();
        }

        private static void OnRadioPlayMessageReceived(PlayCardViewModel vm, Messages.RadioPlayMessage message)
        {
            Dispatcher.UIThread.Post(() =>
            {
                vm.Url = message.Value.Url;
                vm.FaviconUrl = message.Value.FaviconUrl;
                vm.Title = message.Value.StationName;
            });
            DbCtx.AddOrUpdateRencentStation(RencentStationEntity.Parse(message.Value));
        }

        private void OnRadioPlayerBufferingChanged(float cache)
        {
            Debug.WriteLine("OnRadioPlayerBufferingChanged:{0}", cache);
            Dispatcher.UIThread.Post(() => Buffering = cache < 100, DispatcherPriority.Render);
        }

        private void OnRadioPlayerTimeChanged(long time)
        {           
            Dispatcher.UIThread.Post(() => PlayTime = time, DispatcherPriority.Render);
        }

        private void OnRadioPlayerStatusChanged(LibVLCSharp.Shared.VLCState state)
        {
            Debug.WriteLine("OnRadioPlayerStatusChanged:{0}",state);
            Dispatcher.UIThread.Post(() =>
            {
                if (state == LibVLCSharp.Shared.VLCState.Playing)
                {
                    IsPlaying = true;
                }
                else
                {
                    IsPlaying = false;
                }
            }, DispatcherPriority.Render);
        }

        private void ReleasePlayer()
        {
            if (RadioPlayer != null)
            {
                RadioPlayer.Stop();
                RadioPlayer.Dispose();
                RadioPlayer.StatusChanged -= OnRadioPlayerStatusChanged;
                RadioPlayer.TimeChanged -= OnRadioPlayerTimeChanged;
                RadioPlayer.BufferingChanged -= OnRadioPlayerBufferingChanged;
                RadioPlayer = null;
            }
        }

    }
}
