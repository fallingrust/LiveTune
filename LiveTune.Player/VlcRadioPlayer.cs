using LibVLCSharp.Shared;

namespace LiveTune.Player
{
    public class VlcRadioPlayer : IRadioPlayer
    {
        private readonly LibVLC _vlc;
        private readonly string _url;
        private readonly Media _media;
        private readonly MediaPlayer _player;

        public event TimeChangedEventArgs? TimeChanged;
        public event BufferingChangedEventArgs? BufferingChanged;
        public event StatusChangedEventArgs? StatusChanged;
        public VlcRadioPlayer(string url)
        {
            _url = url;
            _vlc = new LibVLC(true, "--network-caching=10000");
            _media = new Media(_vlc, new Uri(_url)); 
            _player = new MediaPlayer(_media);

            _player.TimeChanged += OnTimeChanged;
            _player.Buffering += OnBuffering;
            _media.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(object? sender, MediaStateChangedEventArgs e)
        {
            StatusChanged?.Invoke(e.State);   
        }

        private void OnBuffering(object? sender, MediaPlayerBufferingEventArgs e)
        {
            BufferingChanged?.Invoke(e.Cache);
        }

        private void OnTimeChanged(object? sender, MediaPlayerTimeChangedEventArgs e)
        {
            TimeChanged?.Invoke(e.Time);
        }

        public void Dispose()
        {
            _player.TimeChanged -= OnTimeChanged;
            _player.Buffering -= OnBuffering;
            _media.StateChanged -= OnStateChanged;
            _player.Dispose();
            _media.Dispose();
            _vlc.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Pause()
        {
            _player.Pause();
        }

        public void Play()
        {
            _player.Play();
        }

        public void SetVolume(int volume)
        {
            _player.Volume = volume;
        }

        public void Stop()
        {
            _player.Stop();
        }
    }
}
