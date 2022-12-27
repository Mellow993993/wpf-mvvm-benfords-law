using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.Common
{
    sealed internal class Web
    {
        #region Constructor
        internal Web() { OpenWebsite(); }
        #endregion

        #region Methods
        private void OpenWebsite()
        {
            _ = Process.Start(new ProcessStartInfo
            { FileName = "https://en.wikipedia.org/wiki/Benford%27s_law", UseShellExecute = true });
        }
        #endregion
    }
}
