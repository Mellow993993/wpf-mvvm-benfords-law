using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.Common
{
    internal class BenfordException : Exception
    {
        public string Information { get; set; }
    }
}
