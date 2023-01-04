using BenfordSet.Common;
using BenfordSet.ViewModel;
using System;
using System.Diagnostics;

namespace BenfordSet.Model
{
    sealed internal class AnalyseController
    {
        #region Fields
        private readonly Stopwatch _stopwatch;
        private readonly double _threshold;
        #endregion

        #region Properties
        internal string TotalTime { get; private set; }
        internal ReadPdf ReadPdf { get; init; }
        internal CountNumbers CountedNumbers { get; private set; }
        internal Calculation Calculation { get; private set; }
        internal Output Output { get; private set; }
        internal Results Results { get; private set; }
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
                ReadPdf = readPdf;
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

        #region Methods "StartAnalyse"
        internal string StartAnalyse()
        {
            CountNumbers();
            CalculateDistribution();
            return SetUpResult() + SetUpOutput();
        }
        #endregion

        #region Private methods
        private void CountNumbers()
        {
            CountedNumbers = new CountNumbers(ReadPdf);
            _stopwatch.Stop();
            TotalTime = _stopwatch.ElapsedMilliseconds.ToString();
            CountedNumbers.SumUpAllNumbers();
        }

        private void CalculateDistribution()
        {
            Calculation = new Calculation(CountedNumbers,_threshold);
            Calculation.StartCalculation();
        }

        private string SetUpOutput()
        {
            Output = new Output(Calculation, _threshold);
            return Output.BuildResultOfAnalysis();
        }

        private string SetUpResult()
        {
            Results = new Results(ReadPdf, CountedNumbers, Calculation, TotalTime);
            return Results.BuildResultHeader();
        }
        #endregion

        #region Invoke Events
        private void OnInformUserOnError()
            => InformUserOnError?.Invoke(this,EventArgs.Empty);
        #endregion
    }
}
