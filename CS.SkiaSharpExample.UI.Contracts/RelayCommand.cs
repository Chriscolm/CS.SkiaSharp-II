using System;
using System.Windows.Input;

namespace CS.SkiaSharpExample.UI.Contracts
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        public RelayCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute) : this(execute)
        {
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
