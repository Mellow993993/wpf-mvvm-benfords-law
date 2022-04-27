using System;
using System.Collections.Generic;
using BenfordSet.Common;


namespace BenfordSet.Model
{
    internal class Calculation 
    {
        internal readonly double[] _BenfordNumbers = { 30.1, 17.6, 12.5, 9.7, 7.9, 6.7, 5.8, 5.1, 4.6 };
        internal double[]? Digits = new double[9];
        internal double[]? Difference = new double[9];
        internal double Threshold { get; private set; }
        internal int CountDeviations { get; private set; }
        internal string CalculationResult { get; private set; } = String.Empty;
        internal CountNumbers CounterObject { get; private set; }
        internal Messages Messages { get => new Messages();}

        public event EventHandler? CheckRequired;
        public event EventHandler? NoCheckRequired;
        internal Calculation() { }
        internal Calculation(CountNumbers countObj, double threshold)
        {
            (CounterObject, Threshold) = (countObj, threshold);
            CheckRequired += Messages.CheckFileRequired;
            NoCheckRequired += Messages.NoCheckFileRequred;
        }

        public void StartCalculation()
        {
            CalculateDistribution();
            Deviation();
            ClassifyResults();
        }

        private void CalculateDistribution()
        {
            for (var k = 0; k <= Digits.Length - 1;  k++)
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
                if (Difference[i] > Threshold)
                    CountDeviations += 1;
            InterpretResults();
        }

        private void InterpretResults()
        {
            if (CountDeviations > 3)
                CheckRequired?.Invoke(this, EventArgs.Empty);

            else
                NoCheckRequired?.Invoke(this, EventArgs.Empty);
        }
    }
}
