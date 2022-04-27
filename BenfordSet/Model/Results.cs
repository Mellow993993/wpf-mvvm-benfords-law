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
            sb.AppendLine("Filename:   " + ReadPdf.OnlyFileName);
            sb.AppendLine("Pages:      " + ReadPdf.NumberOfPages);
            sb.AppendLine("Nnumbers:   " + CountNumbers.NumbersInFile);
            sb.AppendLine("Issues:     " + Calculation.CountDeviations);
            sb.AppendLine("Threshold:  " + Calculation.Threshold + " %");
            sb.AppendLine("Time:       " + TotalTime + " ms");
            sb.AppendLine("--------------------------------");
            sb.AppendLine("Distribution");

            sb.AppendLine("Benford\t\tYours\t\t\tDifference");
            return sb.ToString();
        }
    }
}
