using BenfordSet.Common;
using BenfordSet.Model;
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
        #region Fields and properties
        private string _filepath;
        public string Filepath
        {
            get => _filepath;
            set
            {
                if (_filepath != value)
                {
                    _filepath = value; 
                    OnPropertyChanged(nameof(Filepath));
                }
                
            }
        }

        private string _content;
        public string Content { get => _content; set => _content = value; }

        private ReadPdf _readPdf;
        public  ReadPdf ReadPdf { get => _readPdf; set => _readPdf = value;}

        private DelegateCommand _analyseCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _selectCommand;
        private DelegateCommand _quitCommand;
        
        public DelegateCommand AnalyseCommand { get => _analyseCommand; }  
        public DelegateCommand SaveCommand { get => _saveCommand; }
        public DelegateCommand SelectCommand { get => _selectCommand; } 
        public DelegateCommand QuitCommand { get => _quitCommand; } 
        #endregion

        public MainWindowViewModel()
        {
            _selectCommand = new DelegateCommand(Select);
            _analyseCommand = new DelegateCommand(Analyse, CanAnalyse);
            _saveCommand = new DelegateCommand(Save, CanSave);
            _quitCommand = new DelegateCommand(Quit);
        }


        #region Button logic
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
            ReadPdf _readPdf = new ReadPdf(Filepath);
            _readPdf.GetFileContent();

            CountNumbers countnumbers = new CountNumbers(_readPdf);
            countnumbers.SumUpAllNumbers();
        }

        private void Quit() => Application.Current.Shutdown();
      
        #endregion


        #region CanExecute mehtods
        private bool CanAnalyse() => !string.IsNullOrWhiteSpace(Filepath);
        private bool CanSave() => !String.IsNullOrEmpty(Filepath) && !String.IsNullOrEmpty(Content);

        #endregion

        private void RaisePropertyChanged([CallerMemberName] string propname = "")
        {
            SelectCommand.OnExecuteChanged();
            AnalyseCommand.OnExecuteChanged();
            SaveCommand.OnExecuteChanged();
            QuitCommand.OnExecuteChanged();
        }
    }
}
