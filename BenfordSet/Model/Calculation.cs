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
        #region "Private Fields"
        private int _countDeviations;

        public int NumberInFiles { get; set; }
        #endregion

        #region "Properties"
        public int CountDeviations { get { return _countDeviations; } private set { _countDeviations = value; } }
        public int[] CountedNumbers { get; private set; }
        public double Threshold { get; set; }
        public double[] BenfordNumbers { get; } = { 30.1, 17.6, 12.5, 9.7, 7.9, 6.7, 5.8, 5.1, 4.6 };
        public double[] Digits { get; private set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public double[] Difference { get; private set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public List<double> ListOfDigits { get; private set; }
        public List<double> ListOfBenfordNumbers { get; private set; }
        public List<double> ListOfDifferance { get; private set; }
        #endregion

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
            CreateLists();
        }

        private void CreateLists()
        {
            ListOfDigits = new List<double>(Digits);
            ListOfBenfordNumbers = new List<double>(BenfordNumbers);
            ListOfDifferance = new List<double>(Difference);
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
    }
}
