using System;
using System.Text;

namespace BenfordSet.Model
{
    class Output : IFormattable
    {
        public double Threshold { get; private set; }
        public Calculation Calculation { get; private set; }

        public Output(Calculation calculation, double threshold)
        {
            Calculation = calculation;
            Threshold = threshold;
        }
        internal string BuildResultOfAnalysis()
        {
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < 8; i++)
            {
                if (Calculation.Difference[i] < Threshold)
                    sb.AppendLine(CombineOutput(i));
                else
                    sb.AppendLine(CombineOutput(i) + "\t####");
            }
            return sb.ToString();
        }
        private string CombineOutput(int i)
            => (Calculation._BenfordNumbers[i] + " %\t\t\t" + Calculation.Digits[i] + " %\t\t\t" + Calculation.Difference[i] + " %").ToString();

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}
