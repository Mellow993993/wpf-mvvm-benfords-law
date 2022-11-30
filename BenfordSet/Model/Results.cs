using System.Text;

namespace BenfordSet.Model
{
    internal class Results
    {
        public string TotalTime { get; private set; }
        public ReadPdf ReadPdf { get; private set; }
        public Calculation Calculation { get; private set; }
        public CountNumbers CountNumbers { get; private set; }

        public Results(ReadPdf readObject,CountNumbers countObject,Calculation calculationObject,string totaltime)
        {
            (ReadPdf, CountNumbers, Calculation, TotalTime) = (readObject, countObject, calculationObject, totaltime);
        }

        public string BuildResultHeader()
        {
            return PrintHeadLine() + PrintMetaInfos();
        }

        private string PrintHeadLine()
        {
            StringBuilder sb = new();
            _ = sb.AppendLine("Results of the Benford analysis.");
            return sb.ToString();
        }

        private string PrintMetaInfos()
        {
            StringBuilder sb = new();
            _ = sb.AppendLine($"Filename:   {ReadPdf.OnlyFileName}");
            _ = sb.AppendLine($"Pages:      {ReadPdf.NumberOfPages}");
            _ = sb.AppendLine($"Numbers:    {CountNumbers.NumbersInFile}");
            _ = sb.AppendLine($"Issues:     {Calculation.CountDeviations}");
            _ = sb.AppendLine($"Threshold:  {Calculation.Threshold} %");
            _ = sb.AppendLine($"Time:       {TotalTime} ms");
            _ = sb.AppendLine("------------------------------------------------------------");
            _ = sb.AppendLine("Distribution");

            _ = sb.AppendLine("Benford\t\tYours\t\t\tDifference");
            return sb.ToString();
        }
    }
}
