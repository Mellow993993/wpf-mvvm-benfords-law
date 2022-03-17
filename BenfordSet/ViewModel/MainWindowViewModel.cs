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
        private Results ?_results;
        private ReadPdf readPdf;
        private DelegateCommand _analyseCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _selectCommand;
        private DelegateCommand _quitCommand;
        private DelegateCommand _infoCommand;
        private DelegateCommand _cancelCommand;

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
        public DelegateCommand AnalyseCommand { get => _analyseCommand; }  
        public DelegateCommand SaveCommand { get => _saveCommand; }
        public DelegateCommand SelectCommand { get => _selectCommand; } 
        public DelegateCommand QuitCommand { get => _quitCommand; } 
        public DelegateCommand InfoCommand { get => _infoCommand; }
        public DelegateCommand CancelCommand { get => _cancelCommand; }
        public Clean Clean { get; set; }
        internal Messages? Messages { get; set; }
        internal Validation? Validation { get; set; }
        #endregion

        public event EventHandler? FileSelected;
        public event EventHandler? NoFileSelected;
        public event EventHandler? IsCanceld;

        public MainWindowViewModel()
        {
            InitializeFields();
            DecleareDelegateCommands();
            CreateObjects();
            RegisterEvents();
        }

        private void InitializeFields()
        {
            _isText = true;
        }

        private void DecleareDelegateCommands()
        {
            _selectCommand = new DelegateCommand(SelectFile);
            _analyseCommand = new DelegateCommand(Analyse, CanAnalyse);
            _saveCommand = new DelegateCommand(SaveFile, CanSave);
            _cancelCommand = new DelegateCommand(Cancel, CanCancel);
            _infoCommand = new DelegateCommand(Info);
            _quitCommand = new DelegateCommand(Quit);
        }

        private void CreateObjects()
        {
            Clean = new ();
            Messages = new ();
            Validation = new Validation();    
        }

        private void RegisterEvents()
        {
            NoFileSelected += Messages.FileHasNotBeenSelected;
            FileSelected += Messages.FileHasBeenSelected;
            IsCanceld += Messages.CancelProcess;
        }


        private void SelectFile()
        {
            Select selectfile = new Select();
            Filepath = selectfile.OpenDialog();

            if (!string.IsNullOrEmpty(Filepath))
            {
                FileSelected?.Invoke(this, EventArgs.Empty);
                RaisePropertyChanged();
            }
            else
            {
                NoFileSelected?.Invoke(this, EventArgs.Empty);
                RaisePropertyChanged();
            }
        }

        private async void Analyse()
        {
            ReadPdf ReadPdf = new(Filepath);
            RaisePropertyChanged();
            await ReadPdf.GetFileContent();

            if (Validation.IsObjectNull(ReadPdf))
                StartAnalyseProcess(ReadPdf);
        }

        private void StartAnalyseProcess(ReadPdf ReadPdf)
        {
            CountNumbers Countnumbers = new(ReadPdf);
            Countnumbers.SumUpAllNumbers();

            Calculation Calculation = new Calculation(Countnumbers, Threshold);
            Calculation.StartCalculation();

            Results result = new Results(ReadPdf, Countnumbers, Calculation);
            CalculationResults = result.BuildResultString();
            RaisePropertyChanged();
        }

        private void SaveFile()
        {
            Save save = new Save(CalculationResults, IsText);
            save.OpenSaveDialog();
            save.SaveFile();
        }

        private void Cancel()
        {
            readPdf.CancelReading = true;
            Clean.DisposeReadObject(ref readPdf);
            IsCanceld?.Invoke(this, EventArgs.Empty);
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


        private void RaisePropertyChanged([CallerMemberName] string propname = "")
        {
            SelectCommand.OnExecuteChanged();
            AnalyseCommand.OnExecuteChanged();
            SaveCommand.OnExecuteChanged();
            CancelCommand.OnExecuteChanged();
            QuitCommand.OnExecuteChanged();
        }

        private void Quit() => Application.Current.Shutdown();
        private bool CanAnalyse() => !string.IsNullOrWhiteSpace(Filepath);
        private bool CanSave() => !string.IsNullOrEmpty(CalculationResults);
        private bool CanCancel() => readPdf != null;
    }
}
