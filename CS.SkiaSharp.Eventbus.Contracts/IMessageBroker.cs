using CS.SkiaSharpExample.Eventbus.Contracts.Events;
using System;

namespace CS.SkiaSharpExample.Eventbus.Contracts
{
    public interface IMessageBroker
    {
        event EventHandler<MessageSendingEventArgs> MessageSending;
        void Send(object sender, EventArgs e);
    }
}
