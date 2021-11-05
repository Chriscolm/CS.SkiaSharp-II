using CS.SkiaSharpExample.Eventbus.Contracts;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CS.SkiaSharpExample.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IMessageBroker MessageBroker { get; }

        public ViewModel(IMessageBroker messageBroker)
        {
            MessageBroker = messageBroker;
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }

        protected void SendMessage(object sender, EventArgs e)
        {
            MessageBroker.Send(this, e);
        }
    }
}
