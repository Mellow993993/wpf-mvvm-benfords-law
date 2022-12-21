﻿using BenfordSet.Common;
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
        private string _filename = string.Empty;
        private int _threshold = 1;
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
                    OnPropertyChanged(nameof(IsText));
                }

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
                    OnPropertyChanged(nameof(SavePath));   
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

                RaisePropertyChanged(nameof(CalculationResults));
                OnPropertyChanged(TotalTime);
            }
        }
        public int Threshold
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
                    OnPropertyChanged(nameof(TotalTime));
                }
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
                    OnPropertyChanged(nameof(Filepath));
                    RaisePropertyChanged();
                }
            }
        }
        public string Filename
        {
            get => _filename;
            set
            {
                if(_filename != value)
                {
                    _filename = value;
                    OnPropertyChanged(nameof(Filename));
                }
            }
        }
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
        public delegate MessageBoxResult OpenMessageboxHandler(string title,string text);
        public OpenMessageboxHandler OpenMessageBox { get; set; }
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
                IsLoading = false;
                CalculationResults = controller.StartAnalyse();
            }
        }

        private void SelectFile()
        {
            Select selectfile = new();
            Filepath = selectfile.OpenSelectDialog();
        }
        private void SaveFile()
        {
            Save save = new(CalculationResults,IsText);

        }
        private void Cancel()
        {
            readPdf.CancelReading = true;
            RaisePropertyChanged();

            Clean Clean = new();
            Clean.DisposeReadObject(ref readPdf); 
            IsLoading = false;
            IsCanceld?.Invoke(this,EventArgs.Empty);
        }

        private void Info()
        {
            Web web = new Web();
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
            if(OpenMessageBox("Do you want to quit the application","Question") == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else
                return;
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
