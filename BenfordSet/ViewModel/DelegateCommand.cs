using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BenfordSet.ViewModel
{
    internal class DelegateCommand : ICommand
    {
        private Action _execute;
        private Func<bool> _canExecute;

        public DelegateCommand(Action execute ) { _execute = execute; }
        public DelegateCommand(Action execute, Func<bool> canexecute) 
        { 
            _execute = execute;
            _canExecute = canexecute;   
        }

        
        public event EventHandler? CanExecuteChanged;

        

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
