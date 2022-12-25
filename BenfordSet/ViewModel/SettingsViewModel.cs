using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.ViewModel
{
    internal class SettingsViewModel : ViewModelBase
    {
        #region Fieleds

        #endregion

        #region Properties

        #endregion

        #region Delegate Commands
        internal DelegateCommand OkCommand { get; init; }
        internal DelegateCommand ExitCommand { get; init; }
        #endregion

        #region Constructor
        internal SettingsViewModel()
        {
            OkCommand = new DelegateCommand(Ok, CanOk);
            ExitCommand = new DelegateCommand(Exit);
        }
        #endregion

        #region Private Methods
        private void Ok()
        {
            throw new NotImplementedException();
        }

        private void Exit()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region CanExecute Methods
        private bool CanOk() => true;
       #endregion
    }
}
