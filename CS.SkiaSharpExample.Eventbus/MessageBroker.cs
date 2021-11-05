using CS.SkiaSharpExample.Eventbus.Contracts;
using CS.SkiaSharpExample.Eventbus.Contracts.Events;
using System;

namespace CS.SkiaSharpExample.Eventbus
{
    public class MessageBroker : IMessageBroker
    {
        public event EventHandler<MessageSendingEventArgs> MessageSending;

        public void Send(object sender, EventArgs e)
        {
            MessageSending?.Invoke(sender, new MessageSendingEventArgs(e));
        }
    }
}
