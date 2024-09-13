using CommunityToolkit.Mvvm.Messaging.Messages;
using LiveTune.Models;

namespace LiveTune.Messages
{
    public class RadioPlayMessage : ValueChangedMessage<StationListItem>
    {
        public RadioPlayMessage(StationListItem value) : base(value)
        {
        }
    }
}
