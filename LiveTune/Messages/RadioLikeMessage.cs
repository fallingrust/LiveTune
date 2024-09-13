using CommunityToolkit.Mvvm.Messaging.Messages;
using LiveTune.Models;

namespace LiveTune.Messages
{
    public class RadioLikeMessage : ValueChangedMessage<StationListItem>
    {
        public RadioLikeMessage(StationListItem value) : base(value)
        {
        }
    }
}
