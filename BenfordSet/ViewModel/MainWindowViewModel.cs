using BenfordSet.Common;
using BenfordSet.Model;
using BenfordSet.View;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BenfordSet.ViewModel
{
    sealed internal class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        private bool _isText = true;
        private bool _isLoading = false;
        private bool _languageIsEngish = true;
        private string _calculationResults = string.Empty;
        private int _threshold = 5;
        private string _filepath = string.Empty;
        private ReadPdf? _readPdf;
        #endregion

        #region Properties
        public ReadPdf? ReadPdf
        {
            get => _readPdf;
            set 
            { 
                if(_readPdf != value)
                {
                    _readPdf = value;
                    RaisePropertyChanged(nameof(ReadPdf));
                }
            }
        }
        public bool LanguageIsEnglish
        {
            get => _languageIsEngish; 
            set 
            { 
                if(_languageIsEngish != value)
                {
                    _languageIsEngish = value;
                    RaisePropertyChanged(nameof(LanguageIsEnglish));

                    OnPropertyChanged();
                }
            }
        }
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
        public string CalculationResults
        {
            get => _calculationResults;
            set
            {
                if(_calculationResults != value)
                {
                    _calculationResults = value;
                    OnPropertyChanged(); // show if something in view has changed
                    RaisePropertyChanged(nameof(CalculationResults));
                }
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
                    RaisePropertyChanged(nameof(Threshold));
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
                    RaisePropertyChanged(nameof(Filepath));
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
        public DelegateCommand SettingCommand { get; init; }
        internal Messages? Messages => new();
        #endregion

        #region Events
        public delegate MessageBoxResult OpenMessageboxHandler(string title,string text);
        public OpenMessageboxHandler OpenMessageBox { get; set; }
        public event EventHandler? IsCanceld;
        public event EventHandler OpenSettingView;
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
            SettingCommand = new DelegateCommand(Setting);
            IsCanceld += Messages.CancelProcess;
        }
        #endregion

        #region Command methods (private)
        private async void Analyse()
        {
            IsLoading = true;
            Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();

            ReadPdf = new ReadPdf(Filepath);
            await ReadPdf.GetFileContent();

            AnalyseController controller = new(ReadPdf,stopwatch,Threshold);
            IsLoading = false;
            CalculationResults = controller.StartAnalyse();
        }

        private void Cancel()
        {
            ReadPdf.CancelReading = true;
            IsLoading = false;
            IsCanceld?.Invoke(this,EventArgs.Empty);
            ReadPdf = null;
            Filepath = string.Empty;
            RaisePropertyChanged();
        }

        private void SelectFile()
        {
            Select selectfile = new();
            Filepath = selectfile.OpenSelectDialog();
        }
        private void Setting() => OpenSettingView?.Invoke(this,EventArgs.Empty);
        private void SaveFile() => _ = new Save(CalculationResults,IsText);
        private void Info() => _ = new Web();
        private void Quit()
        {
            if(OpenMessageBox("Do you want to quit the application","Question") == MessageBoxResult.Yes)
                Application.Current.Shutdown();
            else
                return;
        }
        #endregion

        #region IsExecutable
        private bool CanAnalyse() => !string.IsNullOrWhiteSpace(Filepath);
        private bool CanSave() => !string.IsNullOrEmpty(CalculationResults);
        private bool CanCancel() => ReadPdf != null;
        #endregion

        #region RaisePropertyChanged
        private void RaisePropertyChanged([CallerMemberName] string propname = "")
        {
            SelectCommand.OnExecuteChanged();
            AnalyseCommand.OnExecuteChanged();
            SaveCommand.OnExecuteChanged();
            CancelCommand.OnExecuteChanged();
            SettingCommand.OnExecuteChanged();
            QuitCommand.OnExecuteChanged();
        }
        #endregion
    }
}
