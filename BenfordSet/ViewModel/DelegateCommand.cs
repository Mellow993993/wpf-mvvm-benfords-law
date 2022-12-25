using System;
using System.Windows.Input;

namespace BenfordSet.ViewModel
{
    internal class DelegateCommand : ICommand
    {
        #region Fields
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        #endregion

        #region Events
        public event EventHandler? CanExecuteChanged;
        #endregion

        #region Constructors
        public DelegateCommand(Action execute) { _execute = execute; }
        public DelegateCommand(Action execute,Func<bool> canexecute) { _execute = execute; _canExecute = canexecute; }
        #endregion

        #region Methods
        public void OnExecuteChanged()
            => CanExecuteChanged?.Invoke(this,new EventArgs());
        public bool CanExecute(object? parameter)
            => _canExecute == null || _canExecute();
        public void Execute(object? parameter)
            => _execute();
        #endregion
    }
}
