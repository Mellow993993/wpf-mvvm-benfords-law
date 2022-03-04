using BenfordSet.Common;
using BenfordSet.Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace BenfordSet.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Fields and properties
        private bool _isText;
        private string _savePath = string.Empty;
        private string _calculationResults = string.Empty;
        private double _threshold = 5;
        private string _filepath = string.Empty;
        private string _fileName = string.Empty;
        private int _numberOfPages = 0;
        private bool _isIndeterminate = true;
        private Results _results;
        private Clean _clean;
        private ReadPdf readPdf;
        private DelegateCommand _analyseCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _selectCommand;
        private DelegateCommand _quitCommand;
        private ProgrammEvents _events;
        private DelegateCommand _infoCommand;
        private DelegateCommand _cancelCommand;

        public bool IsIndeterminate
        {
            get => _isIndeterminate;
            set
            {
                if(_isIndeterminate != value)
                {
                    _isIndeterminate = value;
                    OnPropertyChanged(nameof(IsIndeterminate));
                }
            }
        }
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
                    _savePath = value;
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
        public double Threshold 
        { 
            get => _threshold;
            set
            {
                if (_threshold != value)
                    _threshold = value; OnPropertyChanged(nameof(Threshold)); CanAnalyse();
            }
        }
        public string Filepath
        {
            get => _filepath;
            set
            {
                if (_filepath != value)
                    _filepath = value;
            }
        }
        public string Filename { get => _fileName; set => _fileName = value; }
        public int NumberOfPages { get => _numberOfPages; set => _numberOfPages = value; }
        public Clean Clean
        {
            get => _clean;
            set => _clean = value;
        }
        public ReadPdf ReadPdf { get => readPdf; set => readPdf = value; }
        public Results Results { get => _results; set => _results = value; }
        public DelegateCommand AnalyseCommand { get => _analyseCommand; }  
        public DelegateCommand SaveCommand { get => _saveCommand; }
        public DelegateCommand SelectCommand { get => _selectCommand; } 
        public DelegateCommand QuitCommand { get => _quitCommand; } 
        public DelegateCommand InfoCommand { get => _infoCommand; }
        public DelegateCommand CancelCommand { get => _cancelCommand; }
        #endregion


        public MainWindowViewModel()
        {
            _isText = true;
            _selectCommand = new DelegateCommand(SelectFile);
            _analyseCommand = new DelegateCommand(Analyse, CanAnalyse);
            _saveCommand = new DelegateCommand(SaveFile, CanSave);
            _infoCommand = new DelegateCommand(Info);
            _quitCommand = new DelegateCommand(Quit);
            _cancelCommand = new DelegateCommand(Cancel, CanCancel);
            _clean = new Clean();
            _events = new ProgrammEvents();
            _events.FileSelected += _events.FileHasBeenSelected;
            _events.NoFileSelected += _events.FileHasNotBeenSelected;
            _events.IsCanceld += _events.CancelProcess;
        }

        private void Cancel()
        {
            readPdf.CancelReading = true;
            _clean.DisposeReadObject(ref readPdf);
            _events.OnCancel();
            RaisePropertyChanged();
        }

        private void Info()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://en.wikipedia.org/wiki/Benford%27s_law",
                UseShellExecute = true
            });
        }

        private void SelectFile()
        {
            Select selectfile = new Select();
            Filepath = selectfile.OpenDialog();
            if (!string.IsNullOrEmpty(Filepath))
            {
                _events.OnFileSelected();
                RaisePropertyChanged();
            }
            else
            {
                _events.OnNoFileSelected();
                RaisePropertyChanged();
            }
        }

        private void SaveFile()
        {
            Save save = new Save(CalculationResults, IsText);
            save.OpenSaveDialog();
            save.SaveFile();
        }

        private async void Analyse()
        {
            readPdf = new ReadPdf(Filepath);
            RaisePropertyChanged();
            IsIndeterminate = true;
            await readPdf.GetFileContent();
            // invoke progressbar thread and stop it, when GetFileContent is ready
            if(readPdf != null)
            {
                StartAnalyseProcess();
            }
            // stop progressbar
        }
        private void Quit() => Application.Current.Shutdown();
        private bool CanAnalyse() => !string.IsNullOrWhiteSpace(Filepath);
        private bool CanSave() => !string.IsNullOrEmpty(CalculationResults);
        private bool CanCancel() => readPdf != null;
    
        private void RaisePropertyChanged([CallerMemberName] string propname = "")
        {
            SelectCommand.OnExecuteChanged();
            AnalyseCommand.OnExecuteChanged();
            SaveCommand.OnExecuteChanged();
            CancelCommand.OnExecuteChanged();
            QuitCommand.OnExecuteChanged();
        }

        private void StartAnalyseProcess()
        {
            IsIndeterminate = false;
            Filename = readPdf.OnlyFileName;
            NumberOfPages = readPdf.NumberOfPages;
            CountNumbers countnumbers = new CountNumbers(readPdf);
            _clean.DisposeReadObject(ref readPdf);
            countnumbers.SumUpAllNumbers();
            Calculation calculate = new Calculation(countnumbers, Threshold);
            calculate.StartCalculation();
            Results result = new Results(calculate, Filename, NumberOfPages);
            CalculationResults = result.BuildResultString();
            RaisePropertyChanged();
        }
    }
}
