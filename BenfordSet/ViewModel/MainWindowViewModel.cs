using BenfordSet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BenfordSet.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string _filepath;
        public string Filepath
        {
            get => _filepath;
            set
            {
                if(_filepath != value)
                {
                    _filepath = value;
                    OnPropertyChanged(nameof(Filepath));
                }
            }
        }

        private DelegateCommand _analyseCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _selectCommand;
        private DelegateCommand _quitCommand;
        
        public DelegateCommand AnalyseCommand { get => _analyseCommand; }  
        public DelegateCommand SaveCommand { get => _saveCommand; }
        public DelegateCommand SelectCommand { get => _selectCommand; } 
        public DelegateCommand QuitCommand { get => _quitCommand; } 

        public MainWindowViewModel()
        {
            _selectCommand = new DelegateCommand(Select, CanSelect);
            _analyseCommand = new DelegateCommand(Analyse, CanAnalyse);
            _saveCommand = new DelegateCommand(Save, CanSave);
            _quitCommand = new DelegateCommand(Quit, CanQuit);
        }

        private bool CanQuit() => true;
      

        private void Quit()
        {
            MessageBox.Show("quit");
        }

        private void Select()
        {
            SelectFile selectfile = new SelectFile();
            Filepath = selectfile.OpenDialog();
            RaisePropertyChanged();        
        }


        private void Save()
        {
            MessageBox.Show("save");
        }

        private void Analyse()
        {
            MessageBox.Show("analyse");
        }

        #region CanExecute mehtods
        private bool CanAnalyse() => !string.IsNullOrWhiteSpace(Filepath) ? true : false;
   
        private bool CanSelect()
            => true;
        private bool CanSave()
            => true;
        #endregion

        private void RaisePropertyChanged([CallerMemberName] string propname = "")
        {
            ((DelegateCommand)AnalyseCommand).OnExecuteChanged();
            ((DelegateCommand)SaveCommand).OnExecuteChanged();
            ((DelegateCommand)SelectCommand).OnExecuteChanged();
        }
    }
}
