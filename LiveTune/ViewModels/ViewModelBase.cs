using CommunityToolkit.Mvvm.ComponentModel;
using LiveTune.Views.Pages;
using System;

namespace LiveTune.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        public static implicit operator ViewModelBase(OnlineStationPage v)
        {
            throw new NotImplementedException();
        }
    }
}


