using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging.Messages;
using LiveTune.Models;

namespace LiveTune.Messages
{
    public class NavigateViewMessage : ValueChangedMessage<ContentControl>
    {
        public NavigateViewMessage(ContentControl value) : base(value)
        {
        }
    }
}
