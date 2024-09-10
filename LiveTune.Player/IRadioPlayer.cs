﻿using LibVLCSharp.Shared;

namespace LiveTune.Player
{
    public interface IRadioPlayer : IDisposable
    {
        event TimeChangedEventArgs? TimeChanged;
        event BufferingChangedEventArgs? BufferingChanged;
        event StatusChangedEventArgs? StatusChanged;
        void Play();

        void Stop();

        void Pause();

        void SetVolume(int volume);
    }

    public delegate void TimeChangedEventArgs(long time);
    public delegate void BufferingChangedEventArgs(float cache);
    public delegate void StatusChangedEventArgs(VLCState state);
}
