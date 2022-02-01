using System.Text;

namespace BenfordSet.Model
{
    class Output : Calculation
     {
        public Output(int[] countedNumbers, double[] digits, double[] difference, double threshold) 
        {
            CountedNumbers = countedNumbers;
            Digits = digits;
            Difference = difference;
            Threshold = threshold;
        }
        internal string BuildAnalyseResult()
        {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < CountedNumbers.Length; i++)
            {
                if (Difference[i] < Threshold)
                    sb.AppendLine(CombineOutput(i));
                else
                    sb.AppendLine(CombineOutput(i) + "\t####");
            }
            return sb.ToString();
        }
        private string CombineOutput(int i)
            => (BenfordNumbers[i] + " %\t\t\t" + Digits[i] + " %\t\t\t" + Difference[i] + " %").ToString();
    }
}
