using System.Text;

namespace BenfordSet.Model
{
    internal class Results : FileAttributes
    {
        public string CalculationResult { get; set; }
        public double Threshold { get; set; }
        public int CountDeviation { get; set; }
        public int AllNumbersInFile { get; set; }

        public Results(Calculation calcObj, string filename, int numberOfPages)
        {
            Threshold = calcObj.Threshold;
            CalculationResult = calcObj.CalculationResult;
            CountDeviation = calcObj.CountDeviations;
            AllNumbersInFile = calcObj.NumberInFiles;
            Filename = filename;
            NumberOfPages = numberOfPages;

        }

        public string BuildResultString()
            => PrintHeadLine() + PrintMetaInfos() + CalculationResult;


        private string PrintHeadLine()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(new string('#', 60));
            sb.AppendLine("Results of the benford analysis.");
            sb.AppendLine(new string('#', 60));
            return sb.ToString();
        }

        private string PrintMetaInfos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Filename:\t\t" + Filename);
            sb.AppendLine("Number of pages:\t" + NumberOfPages);
            sb.AppendLine("All numbers in file:\t" + AllNumbersInFile);
            sb.AppendLine("Number of issues:\t" + CountDeviation);
            sb.AppendLine("Threshold:\t\t" + Threshold + " %\n");
            sb.AppendLine("Benford Distribution\tYour Distribution\t\tDifference");
            return sb.ToString();
        }
    }
}
