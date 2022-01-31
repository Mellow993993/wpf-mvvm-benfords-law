using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.Model
{
    internal class Results : FileAttributes
    {

        public string CalculationResult { get; set; }
        public double Threshold { get; set; }
        
        private int _countDeviation;
        public int CountDeviation { get; set; }

        public int AllNumbersInFile { get; set; }

        public Results(Calculation calcObj)
        {
            Threshold = calcObj.Threshold;
            CalculationResult = calcObj.CalculationResult;
            CountDeviation = calcObj.CountDeviations;
            AllNumbersInFile = calcObj.NumberInFiles;
        }

        public string BuildResultString()
            => BuildHeader() + CalculationResult;


        private string BuildHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(new string('#', 60));
            sb.AppendLine("Results of the benford analysis.");
            sb.AppendLine(new string('#', 60));
            sb.AppendLine("Filename:\t");
            sb.AppendLine("All numbers:\t " + AllNumbersInFile);
            sb.AppendLine("Deviation:\t " + CountDeviation);
            sb.AppendLine("Threshold:\t " + Threshold + " %\n");
            sb.AppendLine("Benford Distribution \t Your Distribution \t\t Difference");
            return sb.ToString();
        }
    }
}
