using CommunityToolkit.Mvvm.Messaging.Messages;
using LiveTune.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveTune.Messages
{
    public class RadioPlayMessage : ValueChangedMessage<StationListItem>
    {
        public RadioPlayMessage(StationListItem value) : base(value)
        {
        }
    }
}
