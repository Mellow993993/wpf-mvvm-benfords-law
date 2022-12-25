using BenfordSet.Common;
using System;
using System.Text;

namespace BenfordSet.Model
{
    internal class Output
    {
        #region Properties
        public double Threshold { get; private set; }
        public Messages Messages { get => new Messages(); }
        public Calculation Calculation { get; private set; }
        #endregion

        #region Events
        public delegate void InformUserEventHandler(object source,EventArgs args);
        public event InformUserEventHandler InformUserOnError;
        #endregion

        #region Constructor
        internal Output(Calculation calculation,double threshold)
        {
            InformUserOnError += Messages.OnInformUserOnError;
            if(calculation != null)
            {
                Calculation = calculation;
                Threshold = threshold;
            }
            else
            {
                OnInformUserOnError();
                throw new BenfordException() { Information = "The calculation object is null" };
            }
        }
        #endregion

        private void OnInformUserOnError()
        {
            if(InformUserOnError != null)
                InformUserOnError(this,EventArgs.Empty);
        }

        internal string BuildResultOfAnalysis()
        {
            StringBuilder sb = new();
            for(int i = 0; i < 8; i++)
                _ = Calculation.Difference[i] < Threshold ? sb.AppendLine(CombineOutput(i)) : sb.AppendLine(CombineOutput(i) + "\t####");
            return sb.ToString();
        }

        private string CombineOutput(int i)
            => (Calculation._BenfordNumbers[i] + " %\t\t\t" + Calculation.Digits[i] + " %\t\t\t" + Calculation.Difference[i] + " %").ToString();
    }
}
