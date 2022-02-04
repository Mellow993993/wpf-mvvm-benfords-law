using BenfordSet.Common;
using BenfordSet.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private string _savePath;
        public string SavePath 
        { 
            get => _savePath;
            set
            {
                if(_savePath != value)
                    _savePath = value; OnPropertyChanged(nameof(SavePath));
            }
        }
        private string _calculationResults;
        public string CalculationResults
        {
            get => _calculationResults;
            private set 
            { 
                if(_calculationResults != value)
                    _calculationResults = value; OnPropertyChanged(nameof(CalculationResults));
            }
        }

        private Results _results;
        public Results Results { get => _results; set => _results = value; }
        private double _threshold = 5;
        public double Threshold 
        { 
            get => _threshold;
            set
            {
                if (_threshold != value && (_threshold > 1 || _threshold < 10))
                {
                    _threshold = value;
                    OnPropertyChanged(nameof(Threshold));
                    CanAnalyse();
                }
                else
                    throw new ArgumentException("Der Wert muss zwischen 1 und 10 liegen");
            }
        }

        private string _filepath;
        public string Filepath
        {
            get => _filepath;
            set
            {
                if (_filepath != value)
                    _filepath = value; OnPropertyChanged(nameof(Filepath)); CanAnalyse();
            }
        }

        private string _fileName = string.Empty;
        public string Filename { get => _fileName; set => _fileName = value; }

        private int _numberOfPages = 0;
        public int NumberOfPages { get => _numberOfPages; set => _numberOfPages = value; }

        private string _content;
        public string Content { get => _content; private set => _content = value; }


        private DelegateCommand _analyseCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _selectCommand;
        private DelegateCommand _quitCommand;
        private DelegateCommand _infoCommand;
        private object readpdf;

        public DelegateCommand AnalyseCommand { get => _analyseCommand; }  
        public DelegateCommand SaveCommand { get => _saveCommand; }
        public DelegateCommand SelectCommand { get => _selectCommand; } 
        public DelegateCommand QuitCommand { get => _quitCommand; } 
        public DelegateCommand InfoCommand { get => _infoCommand; }
        #endregion

        public MainWindowViewModel()
        {
            _selectCommand = new DelegateCommand(SelectFile);
            _analyseCommand = new DelegateCommand(Analyse, CanAnalyse);
            _saveCommand = new DelegateCommand(SaveFile, CanSave);
            _infoCommand = new DelegateCommand(Info);
            _quitCommand = new DelegateCommand(Quit);
            // select destination
            //UserSettings usersettings = new UserSettings();
            // save destination   Destination = usersettings.ReadRegistry();
        }


        #region Button logic
        private void Info()
            => Process.Start(new ProcessStartInfo { FileName = "https://en.wikipedia.org/wiki/Benford%27s_law", UseShellExecute = true });
        
        private void SelectFile()
        {
            Select selectfile = new Select();
            Filepath = selectfile.OpenDialog();
            RaisePropertyChanged();        
        }

        private void SaveFile()
        {
            Save save = GetSave();
            save.OpenSaveDialog();
            //save.SaveAsText();
        }

        private Save GetSave()
            => new Save(CalculationResults);

        private void Analyse()
        {
            ReadPdf readPdf = new ReadPdf(Filepath);
            readPdf.GetFileContent(); 
            Filename = readPdf.OnlyFileName;
            NumberOfPages = readPdf.NumberOfPages;

            CountNumbers countnumbers = new CountNumbers(readPdf);

            DisposeReadObject(readPdf);

            countnumbers.SumUpAllNumbers();

            Calculation calculate = new Calculation(countnumbers, Threshold);
            calculate.StartCalculation();

            Results result = new Results(calculate, Filename, NumberOfPages );
            CalculationResults =  result.BuildResultString();
        }

        private void DisposeReadObject(ReadPdf readpdf)
        {
            if (readpdf != null)
            {
                readpdf = null;
                GC.Collect();
            }
        }

        private void Quit() => Application.Current.Shutdown();
        #endregion


        #region CanExecute mehtods
        private bool CanAnalyse()
            => !string.IsNullOrWhiteSpace(Filepath); // && Threshold > 0;
        private bool CanSave() => true; //!String.IsNullOrEmpty(Filepath) && !String.IsNullOrEmpty(Content);
        // implement if obj != null then enable save button

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
