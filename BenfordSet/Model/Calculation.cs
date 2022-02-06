using System;

namespace BenfordSet.Model
{
    internal class Calculation : FileAttributes
    {
        public int NumberInFiles { get; set; }
        public int CountDeviations { get; private set; }
        public int[] CountedNumbers { get; protected set; }
        public double Threshold { get; set; }
        public double[] BenfordNumbers { get; } = { 30.1, 17.6, 12.5, 9.7, 7.9, 6.7, 5.8, 5.1, 4.6 };
        public double[] Digits { get; protected set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public double[] Difference { get; protected set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public string CalculationResult { get; set; }

        public Calculation() { }
        public Calculation(CountNumbers countObj, double threshold)
        {
            NumberInFiles = countObj.NumbersInFile;
            Content = countObj.Content;
            CountedNumbers = countObj.FoundNumbers;
            Threshold = threshold;
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
            Output output = new Output(CountedNumbers, Digits, Difference, Threshold);
            CalculationResult = output.BuildAnalyseResult();
        }

    }
}
