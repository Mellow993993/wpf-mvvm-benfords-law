using BenfordSet.Common;
using BenfordSet.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BenfordSet.Model
{
    internal class AnalyseController
    {
        private readonly ReadPdf _readPdf;
        private readonly Timing _timing;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private double _threshold;

        internal AnalyseController(ReadPdf readPdf, Timing timing, double threshold)
        {
            _readPdf = readPdf;
            _timing = _timing;
            _threshold = threshold;
        }

        private CountNumbers _countNumbers;
        private Calculation _calculation;
        private Output _output;
        private Results _result;
        internal string StartAnalyse()
        {
            CountNumbers();
            CalculateDistribution();

            return SetUpResult() + SetUpOutput();
        }

        private string SetUpOutput()
        {
            _output = new Output(_calculation, _threshold);
            return _output.BuildResultOfAnalysis();
        }

        private string SetUpResult()
        {
            var TotalTime = "10"; //= _timing.StopTimeMeasurement();
            _result = new Results(_readPdf, _countNumbers, _calculation, TotalTime);
            return _result.BuildResultHeader();

        }

        private void CalculateDistribution()
        {
            _calculation = new Calculation(_countNumbers, _threshold);
            _calculation.StartCalculation();
        }

        private void CountNumbers()
        {
            _countNumbers = new CountNumbers(_readPdf);
            _countNumbers.SumUpAllNumbers();
        }
    }
}
