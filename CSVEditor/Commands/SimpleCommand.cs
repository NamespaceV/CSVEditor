using System;
using System.Windows.Input;

namespace CSVEditor.Commands
{
    public class SimpleCommand : ICommand
    {
        private readonly Action _a;

        public event EventHandler CanExecuteChanged;

        public SimpleCommand(Action a)
        {
            _a = a;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _a();
        }
    }
}
