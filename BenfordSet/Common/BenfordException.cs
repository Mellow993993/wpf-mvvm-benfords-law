using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.Common
{
    sealed internal class BenfordException : Exception
    {
        internal string Information { get; set; }
    }
}
