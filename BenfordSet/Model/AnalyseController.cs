using BenfordSet.Common;
using BenfordSet.ViewModel;

namespace BenfordSet.Model
{
    internal class AnalyseController
    {
        #region Fields
        private readonly ReadPdf _readPdf;
        private readonly Timing _timing;
        private readonly double _threshold;
        private CountNumbers _countNumbers;
        private Calculation _calculation;
        private Output _output;
        private Results _result;
        #endregion

        #region Constructor
        internal AnalyseController(ReadPdf readPdf,Timing timing,double threshold)
        {
            _readPdf = readPdf;
            _timing = timing;
            _threshold = threshold;
        }
        #endregion

        #region Methods
        internal string StartAnalyse()
        {
            CountNumbers();
            CalculateDistribution();

            return SetUpResult() + SetUpOutput();
        }

        private string SetUpOutput()
        {
            _output = new Output(_calculation,_threshold);
            return _output.BuildResultOfAnalysis();
        }

        private string SetUpResult()
        {
            string TotalTime = _timing.StopTimeMeasurement(); // BUG-FIX REQUIRED
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
            _countNumbers.SumUpAllNumbers();
        }
        #endregion
    }
}
