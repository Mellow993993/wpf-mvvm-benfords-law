using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.Logging
{
    internal interface ILog
    {
        void Log(string message);
        string WriteToFile();
    }
}
