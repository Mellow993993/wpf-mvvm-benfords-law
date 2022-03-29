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
        private bool _isText = true;
        private string _savePath = string.Empty;
        private string _calculationResults = string.Empty;
        private double _threshold = 5;
        private string _filepath = string.Empty;
        private ReadPdf readPdf;

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
        public string Filename { get; set; }
        public DelegateCommand AnalyseCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand SelectCommand { get; }
        public DelegateCommand QuitCommand { get; }
        public DelegateCommand InfoCommand { get => InfoCommand1; }
        public DelegateCommand CancelCommand { get; }
        public Clean Clean { get; set; }
        internal Messages? Messages { get => new Messages(); }
        internal Validation? Validation { get => new Validation(); }

        internal DelegateCommand InfoCommand1 { get; }

        public event EventHandler? FileSelected;
        public event EventHandler? NoFileSelected;
        public event EventHandler? IsCanceld;

        public MainWindowViewModel()
        {
            SelectCommand = new DelegateCommand(SelectFile);
            AnalyseCommand = new DelegateCommand(Analyse, CanAnalyse);
            SaveCommand = new DelegateCommand(SaveFile, CanSave);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
            InfoCommand1 = new DelegateCommand(Info);
            QuitCommand = new DelegateCommand(Quit);
            NoFileSelected += Messages.FileHasNotBeenSelected;
            FileSelected += Messages.FileHasBeenSelected;
            IsCanceld += Messages.CancelProcess;
        }

        private void SelectFile()
        {
            Select selectfile = new Select();
            Filepath = selectfile.OpenDialog();

            if (!string.IsNullOrEmpty(Filepath))
                FileSelected?.Invoke(this, EventArgs.Empty);
            else
                NoFileSelected?.Invoke(this, EventArgs.Empty);
            RaisePropertyChanged();
        }

        private async void Analyse()
        {
            var readPdf = new ReadPdf(Filepath);
            RaisePropertyChanged();
            await readPdf.GetFileContent();

            if (Validation.IsObjectNull(readPdf))
                StartAnalyseProcess(readPdf);
        }

        private void StartAnalyseProcess(ReadPdf readPdf)
        {
            var Countnumbers = new CountNumbers(readPdf);
            Countnumbers.SumUpAllNumbers();

            var Calculation = new Calculation(Countnumbers, Threshold);
            Calculation.StartCalculation();

            var Result = new Results(readPdf, Countnumbers, Calculation);
            CalculationResults = Result.BuildResultHeader();

            var Output = new Output(Calculation, Threshold);
            var mainInformations  = Output.BuildResultOfAnalysis();

            //Clean Clean = new Clean(ref readPdf, ref Countnumbers, ref Calculation, ref Result);
            CalculationResults = CalculationResults + mainInformations;
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
            RaisePropertyChanged();
            Clean.DisposeReadObject(ref readPdf);
            IsCanceld?.Invoke(this, EventArgs.Empty);
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
