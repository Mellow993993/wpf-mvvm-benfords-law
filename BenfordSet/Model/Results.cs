using System.Text;

namespace BenfordSet.Model
{
    internal class Results 
    {
        public string TotalTime { get; private set; }
        public ReadPdf ReadPdf { get; private set; }
        public Calculation Calculation { get; private set; }
        public CountNumbers CountNumbers { get; private set; } 

        public Results(ReadPdf readObject, CountNumbers countObject, Calculation calculationObject, string totaltime) 
            => (ReadPdf, CountNumbers, Calculation, TotalTime) = (readObject, countObject, calculationObject, totaltime);
        

        public string BuildResultHeader()
            => PrintHeadLine() + PrintMetaInfos();

        private string PrintHeadLine()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Results of the Benford analysis.");
            return sb.ToString();
        }

        private string PrintMetaInfos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Filename:\t\t" + ReadPdf.OnlyFileName);
            sb.AppendLine("Number of pages:\t" + ReadPdf.NumberOfPages);
            sb.AppendLine("All numbers in file:\t" + CountNumbers.NumbersInFile);
            sb.AppendLine("Number of issues:\t" + Calculation.CountDeviations);
            sb.AppendLine("Threshold:\t\t" + Calculation.Threshold + " %");
            sb.AppendLine("Time of the analysis: \t" + TotalTime + " ms\n");
            sb.AppendLine("Benford Distribution\tYour Distribution\t\tDifference");
            return sb.ToString();
        }
    }
}
