using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.Model
{
    internal class Results
    {

        public string CalculationResults { get; set; }
        public string Deviation { get; set; }


        public Results(Calculation calculate) => Deviation = calculate.ListOfBenfordNumbers.ToString();

        public string BuildResultString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("huhu ");
            sb.Append("huhu ");
            sb.Append(Deviation);
            return sb.ToString();

        }
    }
}
