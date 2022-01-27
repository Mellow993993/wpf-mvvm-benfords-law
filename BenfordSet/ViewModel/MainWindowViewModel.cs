using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
                    OnPropertyChanged(Filepath);
                }
            }
        }

        private DelegateCommand _analyseCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _selectCommand;
        
        public DelegateCommand AnalyseCommand { get => _analyseCommand; }  
        public DelegateCommand SaveCommand { get => _saveCommand; }
        public DelegateCommand SelectCommand { get => _selectCommand; } 

        public MainWindowViewModel()
        {
            _analyseCommand = new DelegateCommand(Analyse, CanAnalyse);
            _saveCommand = new DelegateCommand(Save, CanSave);
            _selectCommand = new DelegateCommand(Select, CanSelect);
        }



        private void Select()
        {
            throw new NotImplementedException();
        }


        private void Save()
        {
            throw new NotImplementedException();
        }

        private void Analyse()
        {

        }

        #region CanExecute mehtods
        private bool CanAnalyse()
            => true;
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
