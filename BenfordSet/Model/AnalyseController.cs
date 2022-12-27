using BenfordSet.Common;
using BenfordSet.ViewModel;
using System;
using System.Diagnostics;

namespace BenfordSet.Model
{
    sealed internal class AnalyseController
    {
        #region Fields
        private readonly ReadPdf _readPdf;
        private readonly Stopwatch _stopwatch;
        private readonly double _threshold;
        private CountNumbers _countNumbers;
        private Calculation _calculation;
        private Output _output;
        private Results _result;
        #endregion

        #region Properties
        internal string TotalTime { get; private set; }
        internal Messages Messages { get => new Messages(); }
        #endregion

        #region Events
        public delegate void InformUserEventHandler(object source,EventArgs args);
        public event InformUserEventHandler InformUserOnError;
        #endregion

        #region Constructor
        internal AnalyseController(ReadPdf readPdf,Stopwatch stopwatch,double threshold)
        {
            InformUserOnError += Messages.OnInformUserOnError;
            if(readPdf != null && stopwatch != null)
            {
                _readPdf = readPdf;
                _stopwatch = stopwatch;
                _threshold = threshold;
            }
            else
            {
                OnInformUserOnError();
                throw new BenfordException() { Information = "The object readpdf or stopwatch is null" };
            }
        }
        #endregion

        #region Methods
        internal string StartAnalyse()
        {
            CountNumbers();
            CalculateDistribution();
            return SetUpResult() + SetUpOutput();
        }
        #endregion

        #region Private methods
        private void OnInformUserOnError()
        {
            if(InformUserOnError != null)
                InformUserOnError(this,EventArgs.Empty);
        }
        private string SetUpOutput()
        {
            _output = new Output(_calculation,_threshold);
            return _output.BuildResultOfAnalysis();
        }

        private string SetUpResult()
        {
            _result = new Results(_readPdf,_countNumbers,_calculation,TotalTime);
            return _result.BuildResultHeader();
        }

        private void CalculateDistribution()
        {
            _calculation = new Calculation(_countNumbers,_threshold);
            _calculation.StartCalculation();
        }

        private void CountNumbers()
        {
            _countNumbers = new CountNumbers(_readPdf);
            _stopwatch.Stop();
            TotalTime = _stopwatch.ElapsedMilliseconds.ToString();
            _countNumbers.SumUpAllNumbers();
        }
        #endregion
    }
}
