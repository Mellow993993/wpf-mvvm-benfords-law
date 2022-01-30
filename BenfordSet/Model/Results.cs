using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.Model
{
    internal class Results : FileAttributes
    {

        public string CalculationResults { get; set; }
        public string Deviation { get; set; }
        public double Threshold { get; set; }

        public double[] BenfordNumbers { get; } = { 30.1, 17.6, 12.5, 9.7, 7.9, 6.7, 5.8, 5.1, 4.6 };
        public double[] Digits { get; private set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public double[] Difference { get; private set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] CountedNumbers { get; private set; }

        private int testNumber = 5;


        public Results(Calculation calcObj)
        {
            BenfordNumbers = calcObj.BenfordNumbers;
            Digits = calcObj.Digits;
            Difference = calcObj.Difference;
            Threshold = calcObj.Threshold;
            CountedNumbers = calcObj.CountedNumbers;
        }

        public string BuildResultString()
        {
            var header = BuildHeader();
            var body = BuildBody();
            return header + body;
        }


        private string BuildHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(new string('#', 60));
            sb.AppendLine("Results of the benford analysis.");
            sb.AppendLine(new string('#', 60));
            sb.AppendLine("Filename:\t" + testNumber);
            sb.AppendLine("All numbers:\t " + CountedNumbers.Length);
            sb.AppendLine("Threshold:\t " + Threshold + " %");
            sb.AppendLine();
            sb.AppendLine("Benford Distribution \t Your Distribution \t\t Difference");
            // trigger event, if deviation is to big
            return sb.ToString();
        }

        private string BuildBody()
        {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < CountedNumbers.Length; i++)
            {
                if (Difference[i] < Threshold)
                {
                    string? res1 = ("{0}: {1} % \t\t {2}: {3} %  \t\t {4}: {5} %", i + 1, BenfordNumbers[i], i + 1, Digits[i], i + 1, Difference[i]).ToString();
                    sb.AppendLine(res1);
                }
                else
                {
                    string? res2 = ("{0}: {1} % \t\t {2}: {3} %  \t\t {4}: {5} %", i + 1, BenfordNumbers[i], i + 1, Digits[i], i + 1, Difference[i]).ToString();
                    sb.AppendLine(res2);
                }
            }
            return sb.ToString();
            //PrintDeviation(calcObj.CountDeviations);
        }
    }
}
