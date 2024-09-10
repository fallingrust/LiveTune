using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
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
        public string FaviconUrl { get => _faviconUrl; set => SetProperty(ref _faviconUrl, value); }
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public bool IsPlaying { get => _isPlaying; set => SetProperty(ref _isPlaying, value); }
        public long PlayTime { get => _playTime; set => SetProperty(ref _playTime, value); }
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
        
        private IRadioPlayer? _radioPlayer;
        
        public PlayCardViewModel()
        {
            WeakReferenceMessenger.Default.Register<PlayCardViewModel, Messages.RadioPlayMessage>(this, OnRadioPlayMessageReceived);
        }

        private void CreatePlayer(string url)
        {
            ReleasePlayer();
            _radioPlayer ??= new VlcRadioPlayer(url);
            _radioPlayer.StatusChanged += OnRadioPlayerStatusChanged;
            _radioPlayer.TimeChanged += OnRadioPlayerTimeChanged;
            _radioPlayer.BufferingChanged += OnRadioPlayerBufferingChanged;
            _radioPlayer.Play();
        }

        private static void OnRadioPlayMessageReceived(PlayCardViewModel vm, Messages.RadioPlayMessage message)
        {
            Dispatcher.UIThread.Post(() =>
            {
                vm.Url = message.Value.Url;
                vm.FaviconUrl = message.Value.FaviconUrl;
                vm.Title = message.Value.StationName;
            });
        }

        private void OnRadioPlayerBufferingChanged(float cache)
        {
            
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
            if (_radioPlayer != null)
            {
                _radioPlayer.Stop();
                _radioPlayer.Dispose();
                _radioPlayer.StatusChanged -= OnRadioPlayerStatusChanged;
                _radioPlayer.TimeChanged -= OnRadioPlayerTimeChanged;
                _radioPlayer.BufferingChanged -= OnRadioPlayerBufferingChanged;
                _radioPlayer = null;
            }
        }

    }
}
