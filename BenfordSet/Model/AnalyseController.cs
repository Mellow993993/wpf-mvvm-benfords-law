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

        internal AnalyseController(ReadPdf readPdf, Timing timing, double threshold, MainWindowViewModel meinmodel)
        {
            _readPdf = readPdf;
            _timing = _timing;
            _threshold = threshold;
            _mainWindowViewModel = meinmodel ;
        }

        internal void StartAnalyse()
        {
            var Countnumbers = new CountNumbers(_readPdf);
            Countnumbers.SumUpAllNumbers();

            var Calculation = new Calculation(Countnumbers, _threshold);
            Calculation.StartCalculation();

            var TotalTime = _timing.StopTimeMeasurement();
            var Result = new Results(_readPdf, Countnumbers, Calculation, TotalTime);
            var CalculationResults = Result.BuildResultHeader();

            var Output = new Output(Calculation, _threshold);
            var mainInformations = Output.BuildResultOfAnalysis();

            CalculationResults = CalculationResults + mainInformations;
            
        }
    }
}
