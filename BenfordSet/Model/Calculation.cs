using System;
using System.Collections.Generic;
using BenfordSet.Common;


namespace BenfordSet.Model
{
    internal class Calculation //: FileAttributes
    {
        public event EventHandler? CheckRequired;
        public event EventHandler? NoCheckRequired;

        private int _countDeviations;
        private double _threshold;
        internal readonly double[] _BenfordNumbers = { 30.1, 17.6, 12.5, 9.7, 7.9, 6.7, 5.8, 5.1, 4.6 };
        internal double[] Digits = new double[9];
        internal double[] Difference = new double[9];
        //public Dictionary<int, int> CountedNumbers { get; protected set; }
        internal double Threshold { get; set; }
        public int CountDeviations { get; set; }
        public string CalculationResult { get; set; } = String.Empty;
        public CountNumbers CounterObject { get; private set; }
        internal Messages Messages { get; set; }
        public int[] CountedNumbers { get; private set; }

        //public object CountedNumbers { get; private set; }

        public Calculation() { }
        public Calculation(CountNumbers countObj, double threshold)
        {
            CounterObject = countObj;
            _threshold = threshold;
        }

        private void RegisterEvents()
        {
            Messages = new();
            CheckRequired += Messages.CheckFileRequired;
            NoCheckRequired += Messages.NoCheckFileRequred;
        }

        public void StartCalculation()
        {
            CalculateDistribution();
            Deviation();
            ClassifyResults();
            //SGetOutput();
        }

        private void CalculateDistribution()
        {
            for (var k = 0; k <= Digits.Length - 1; k++)
                Digits[k] = Math.Round(ConvertTypes(CounterObject.FoundNumbers[k]) / CounterObject.NumbersInFile * 100, 1);
        }

        private double ConvertTypes(int numbers) => numbers;

        private void Deviation()
        {
            for (var k = 0; k < _BenfordNumbers.Length; k++)
                Difference[k] = Math.Round(Math.Abs(_BenfordNumbers[k] - Digits[k]), 1);
        }

        private void ClassifyResults()
        {
            for (int i = 0; i <= _BenfordNumbers.Length - 1; i++)
                if (Difference[i] > _threshold)
                    _countDeviations += 1;

            InterpretResults();
        }

        private void InterpretResults()
        {
            if (_countDeviations > 3)
                this.CheckRequired?.Invoke(this, EventArgs.Empty);

            else
                this.NoCheckRequired?.Invoke(this, EventArgs.Empty);
        }

        //private void GetOutput()
        //{
        //    Output output = new Output(CountedNumbers, Digits, Difference, _threshold);
        //    CalculationResult = output.BuildAnalyseResult();
        //}
    }
}
