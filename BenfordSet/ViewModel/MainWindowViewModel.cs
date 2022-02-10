using BenfordSet.Common;
using BenfordSet.Model;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BenfordSet.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Fields and properties
        private bool _isText;
        private string _savePath;
        private string _calculationResults;
        private double _threshold = 5;
        private string _filepath;
        private string _fileName = string.Empty;
        private int _numberOfPages = 0;
        private Results _results;
        private DelegateCommand _analyseCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _selectCommand;
        private DelegateCommand _quitCommand;
        private DelegateCommand _infoCommand;

        public bool IsText
        {
            get => _isText;
            set
            {
                if(_isText != value)
                    _isText = value; OnPropertyChanged(nameof(IsText)); 
            }
        }
        public string SavePath 
        { 
            get => _savePath;
            set
            {
                if(_savePath != value)
                    _savePath = value; OnPropertyChanged(nameof(SavePath));
            }
        }
        public string CalculationResults
        {
            get => _calculationResults;
            set 
            { 
                if(_calculationResults != value)
                    _calculationResults = value; OnPropertyChanged(nameof(CalculationResults)); CanSave();
            }
        }
        public Results Results { get => _results; set => _results = value; }
        public double Threshold 
        { 
            get => _threshold;
            set
            {
                if (_threshold != value && (_threshold > 1 || _threshold < 10))
                    _threshold = value; OnPropertyChanged(nameof(Threshold)); CanAnalyse();
            }
        }
        public string Filepath
        {
            get => _filepath;
            set
            {
                if (_filepath != value)
                    _filepath = value; OnPropertyChanged(nameof(Filepath)); CanAnalyse();
            }
        }
        public string Filename { get => _fileName; set => _fileName = value; }
        public int NumberOfPages { get => _numberOfPages; set => _numberOfPages = value; }

        public DelegateCommand AnalyseCommand { get => _analyseCommand; }  
        public DelegateCommand SaveCommand { get => _saveCommand; }
        public DelegateCommand SelectCommand { get => _selectCommand; } 
        public DelegateCommand QuitCommand { get => _quitCommand; } 
        public DelegateCommand InfoCommand { get => _infoCommand; }
        #endregion

        public MainWindowViewModel()
        {
            _isText = true;
            _selectCommand = new DelegateCommand(SelectFile);
            _analyseCommand = new DelegateCommand(Analyse, CanAnalyse);
            _saveCommand = new DelegateCommand(SaveFile, CanSave);
            _infoCommand = new DelegateCommand(Info);
            _quitCommand = new DelegateCommand(Quit);
        }


        #region Button logic
        private void Info()
        {
            MessageBox.Show(IsText.ToString());
            //Process.Start(new ProcessStartInfo { FileName = "https://en.wikipedia.org/wiki/Benford%27s_law", UseShellExecute = true });
        }

        private void SelectFile()
        {
            Select selectfile = new Select();
            Filepath = selectfile.OpenDialog();
            RaisePropertyChanged();        
        }

        private void SaveFile()
        {
            Save save = new Save(CalculationResults, IsText);
            save.OpenSaveDialog();
            save.SaveFile();
        }

        private async void Analyse()
        {
            ReadPdf readPdf = new ReadPdf(Filepath);
            await readPdf.GetFileContent();
            Filename = readPdf.OnlyFileName;
            NumberOfPages = readPdf.NumberOfPages;
            CountNumbers countnumbers = new CountNumbers(readPdf);
            DisposeReadObject(readPdf);
            countnumbers.SumUpAllNumbers();
            Calculation calculate = new Calculation(countnumbers, Threshold);
            calculate.StartCalculation();
            Results result = new Results(calculate, Filename, NumberOfPages);
            CalculationResults =  result.BuildResultString();
        }

        private void DisposeReadObject(ReadPdf? readpdf)
        {
            if (readpdf != null)
                readpdf = null; GC.Collect();
        }

        private void Quit() => Application.Current.Shutdown();
        #endregion

        #region CanExecute mehtods
        private bool CanAnalyse() => !string.IsNullOrWhiteSpace(Filepath);
        private bool CanSave() => true; //!string.IsNullOrEmpty(CalculationResults); 
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
