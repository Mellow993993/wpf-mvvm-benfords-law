using System.Text;

namespace BenfordSet.Model
{
    internal class Output
    {
        public double Threshold { get; private set; }
        public Calculation Calculation { get; private set; }

        public Output(Calculation calculation,double threshold)
        {
            Calculation = calculation;
            Threshold = threshold;
        }
        internal string BuildResultOfAnalysis()
        {
            StringBuilder sb = new();
            for(int i = 0; i < 8; i++)
            {
                _ = Calculation.Difference[i] < Threshold ? sb.AppendLine(CombineOutput(i)) : sb.AppendLine(CombineOutput(i) + "\t####");
            }
            return sb.ToString();
        }
        private string CombineOutput(int i)
        {
            return (Calculation._BenfordNumbers[i] + " %\t\t\t" + Calculation.Digits[i] + " %\t\t\t" + Calculation.Difference[i] + " %").ToString();
        }
    }
}
