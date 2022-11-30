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
        private bool _isLoading = false;
        private string _savePath = string.Empty;
        private string _calculationResults = string.Empty;
        private double _threshold = 5;
        private string _filepath = string.Empty;
        private string _totalTime = string.Empty;
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
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if(_isLoading != value)
                {
                    _isLoading = value; 
                    OnPropertyChanged(nameof(IsLoading));
                }
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
        public string TotalTime
        {
            get => _totalTime;
            private set 
            { 
                if(_totalTime != null)
                    _totalTime = value; OnPropertyChanged(nameof(TotalTime));
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
        public DelegateCommand InfoCommand { get; }
        public DelegateCommand CancelCommand { get; }
        //public Clean? Clean { get => new Clean(); }
        internal Messages? Messages { get => new(); }
        internal Validation? Validation { get => new (); }

        public event EventHandler? FileSelected;
        public event EventHandler? NoFileSelected;
        public event EventHandler? IsCanceld;

        public MainWindowViewModel()
        {
            SelectCommand = new DelegateCommand(SelectFile);
            AnalyseCommand = new DelegateCommand(Analyse, CanAnalyse);
            SaveCommand = new DelegateCommand(SaveFile, CanSave);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
            InfoCommand = new DelegateCommand(Info);
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
            IsLoading = true;

            Timing timing = new Timing(new Stopwatch());
            timing.StartTimeMeasurement();

            readPdf = new ReadPdf(Filepath);
            //RaisePropertyChanged();
            await readPdf.GetFileContent();

            if (Validation.IsObjectNull(readPdf))
            {
                StartAnalyseProcess(readPdf, timing);
                //AnalyseController controller = new(readPdf, timing, 5);
                //controller.StartAnalyse();
            }
            IsLoading = false;
        }

        private void StartAnalyseProcess(ReadPdf readPdf, Timing timing)
        {
            var Countnumbers = new CountNumbers(readPdf);
            Countnumbers.SumUpAllNumbers();

            var Calculation = new Calculation(Countnumbers, Threshold);
            Calculation.StartCalculation();

            TotalTime = timing.StopTimeMeasurement();
            var Result = new Results(readPdf, Countnumbers, Calculation, TotalTime);
            CalculationResults = Result.BuildResultHeader();

            var Output = new Output(Calculation, Threshold);
            var mainInformations = Output.BuildResultOfAnalysis();

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
            //internal Clean(ref ReadPdf read, ref CountNumbers count, ref Calculation calc, ref Results result)

            Clean Clean = new Clean();// (ref readPdf); //, ref  Calculation, ref Results);
            Clean.DisposeReadObject(ref readPdf); // (ref readPdf);
            IsLoading = false;
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
