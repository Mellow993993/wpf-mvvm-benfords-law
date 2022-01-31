using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BenfordSet.Model
{
    internal class Calculation : FileAttributes
    {
        public int NumberInFiles { get; set; }
        public int CountDeviations { get; private set; }
        public int[] CountedNumbers { get; private set; }
        public double Threshold { get; set; }
        public double[] BenfordNumbers { get; } = { 30.1, 17.6, 12.5, 9.7, 7.9, 6.7, 5.8, 5.1, 4.6 };
        public double[] Digits { get; protected set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public double[] Difference { get; protected set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public string CalculationResult { get; set; }

        public Calculation() { }
        public Calculation(CountNumbers countObj)
        {
            NumberInFiles = countObj.NumbersInFile;
            Content = countObj.Content;
            CountedNumbers = countObj.FoundNumbers;
            Threshold = 1.5;
        }

        public void StartCalculation()
        {
            CalculateDistribution();
            Deviation();
            ClassifyResults();
            GetOutput();
        }


        private void CalculateDistribution()
        {
            for (var k = 0; k <= Digits.Length - 1; k++)
                Digits[k] = Math.Round(ConvertTypes(CountedNumbers[k]) / NumberInFiles * 100, 1);
        }

        private double ConvertTypes(int numbers)
            => (double)numbers;

        private void Deviation()
        {
            for (var k = 0; k < BenfordNumbers.Length; k++)
                Difference[k] = Math.Round(Math.Abs(BenfordNumbers[k] - Digits[k]), 1);
        }

        private void ClassifyResults()
        {
            for (int i = 0; i <= BenfordNumbers.Length - 1; i++)
                if (Difference[i] > Threshold)
                    CountDeviations += 1;
        }

        private void GetOutput()
        {
            Output output = new Output(CountedNumbers.Length, Digits, Difference, Threshold);
            CalculationResult = output.BuildAnalyseResult();
        }

    }

     class Output : Calculation
     {
        private int _length;
        public Output(int length, double[] digits, double[] difference, double threshold) 
        {
            _length = length;
            Digits = digits;
            Difference = difference;
            Threshold = threshold;
        }
        internal string BuildAnalyseResult()
        {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < _length; i++)
            {
                if (Difference[i] < Threshold)
                    sb.AppendLine(CombineOutput(i));
                else
                    sb.AppendLine(CombineOutput(i));
            }
            return sb.ToString();
        }
        private string CombineOutput(int i)
            => (BenfordNumbers[i] + " %\t\t\t" + Digits[i] + " %\t\t\t" + Difference[i] + " %").ToString();
    }
}
