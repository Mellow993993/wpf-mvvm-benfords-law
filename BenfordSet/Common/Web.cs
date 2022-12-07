using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.Common
{
    internal class Web
    {
        public Web()
        {
            OpenWebsite();
        }
           
        internal void OpenWebsite()
        {
            _ = Process.Start(new ProcessStartInfo
            {
                FileName = "https://en.wikipedia.org/wiki/Benford%27s_law",
                UseShellExecute = true
            });
        }
    }
}
