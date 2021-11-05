using System;

namespace CS.SkiaSharpExample.Eventbus.Contracts.Events
{
    public class MessageSendingEventArgs : EventArgs
    {
        public EventArgs Args { get; }

        public MessageSendingEventArgs(EventArgs args)
        {
            Args = args;
        }
    }
}
