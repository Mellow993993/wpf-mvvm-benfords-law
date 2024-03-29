﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.ViewModel
{
    sealed internal class SettingsViewModel : ViewModelBase
    {
        public event EventHandler OkEvent;
        public event EventHandler ExitEvent;

        #region Fieleds
        private bool _languageIsEnglish = true;
        #endregion

        #region Properties
        enum Language
        {
            English,
            Deutsch
        }
        public bool LanguageIsEnglish
        {
            get => _languageIsEnglish; 
            set 
            {
                if(_languageIsEnglish != value)
                {
                    _languageIsEnglish = value; 
                    OnPropertyChanged();
                }
            }
        }
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
            OkEvent?.Invoke(this,EventArgs.Empty);
        }

        private void Exit()
        {
            if(ExitEvent != null)
            {
                ExitEvent(this,EventArgs.Empty);   
            }
        }
        #endregion

        #region CanExecute Methods
        private bool CanOk() => true;
       #endregion
    }
}
