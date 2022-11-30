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
        #region Fields
        private bool _isText = true;
        private bool _isLoading = false;
        private string _savePath = string.Empty;
        private string _calculationResults = string.Empty;
        private double _threshold = 5;
        private string _filepath = string.Empty;
        private string _totalTime = string.Empty;
        private ReadPdf readPdf;
        #endregion

        #region Properties
        public bool IsText
        {
            get => _isText;
            set
            {
                if(_isText != value)
                {
                    _isText = value;
                }

                OnPropertyChanged(nameof(IsText));
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
                {
                    _savePath = value;
                }
            }
        }
        public string CalculationResults
        {
            get => _calculationResults;
            set
            {
                if(_calculationResults != value)
                {
                    _calculationResults = value;
                }

                OnPropertyChanged(nameof(CalculationResults));
                _ = CanSave();
            }
        }
        public double Threshold
        {
            get => _threshold;
            set
            {
                if(_threshold != value)
                {
                    _threshold = value;
                }

                OnPropertyChanged(nameof(Threshold));
                _ = CanAnalyse();
            }
        }
        public string TotalTime
        {
            get => _totalTime;
            private set
            {
                if(_totalTime != null)
                {
                    _totalTime = value;
                }

                OnPropertyChanged(nameof(TotalTime));
            }
        }
        public string Filepath
        {
            get => _filepath;
            set
            {
                if(_filepath != value)
                {
                    _filepath = value;
                }
            }
        }
        public string Filename { get; set; }
        #endregion

        #region DelegateCommands
        public DelegateCommand AnalyseCommand { get; init; }
        public DelegateCommand SaveCommand { get; init; }
        public DelegateCommand SelectCommand { get; init; }
        public DelegateCommand QuitCommand { get; init; }
        public DelegateCommand InfoCommand { get; init; }
        public DelegateCommand CancelCommand { get; init; }
        internal Messages? Messages => new();
        #endregion

        #region Events
        public event EventHandler? FileSelected;
        public event EventHandler? NoFileSelected;
        public event EventHandler? IsCanceld;
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            SelectCommand = new DelegateCommand(SelectFile);
            AnalyseCommand = new DelegateCommand(Analyse,CanAnalyse);
            SaveCommand = new DelegateCommand(SaveFile,CanSave);
            CancelCommand = new DelegateCommand(Cancel,CanCancel);
            InfoCommand = new DelegateCommand(Info);
            QuitCommand = new DelegateCommand(Quit);
            NoFileSelected += Messages.FileHasNotBeenSelected;
            FileSelected += Messages.FileHasBeenSelected;
            IsCanceld += Messages.CancelProcess;
        }
        #endregion

        #region Methods
        private async void Analyse()
        {
            IsLoading = true;
            Timing timing = new(new Stopwatch());
            timing.StartTimeMeasurement();

            readPdf = new ReadPdf(Filepath);
            await readPdf.GetFileContent();

            if(readPdf != null)
            {
                AnalyseController controller = new(readPdf,timing,Threshold);
                CalculationResults = controller.StartAnalyse();
            }
            IsLoading = false;
        }

        private void SelectFile()
        {
            Select selectfile = new();
            Filepath = selectfile.OpenDialog();

            if(!string.IsNullOrEmpty(Filepath))
            {
                FileSelected?.Invoke(this,EventArgs.Empty);
            }
            else
            {
                NoFileSelected?.Invoke(this,EventArgs.Empty);
            }

            RaisePropertyChanged();
        }
        private void SaveFile()
        {
            Save save = new(CalculationResults,IsText);
            save.OpenSaveDialog();
            save.SaveFile();
        }
        private void Cancel()
        {
            readPdf.CancelReading = true;
            RaisePropertyChanged();
            //internal Clean(ref ReadPdf read, ref CountNumbers count, ref Calculation calc, ref Results result)

            Clean Clean = new();// (ref readPdf); //, ref  Calculation, ref Results);
            Clean.DisposeReadObject(ref readPdf); // (ref readPdf);
            IsLoading = false;
            IsCanceld?.Invoke(this,EventArgs.Empty);
        }

        private void Info()
        {
            _ = Process.Start(new ProcessStartInfo
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
        #endregion

        #region IsExecutable
        private void Quit()
        {
            Application.Current.Shutdown();
        }

        private bool CanAnalyse()
        {
            return !string.IsNullOrWhiteSpace(Filepath);
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(CalculationResults);
        }

        private bool CanCancel()
        {
            return readPdf != null;
        }
        #endregion
    }
}
