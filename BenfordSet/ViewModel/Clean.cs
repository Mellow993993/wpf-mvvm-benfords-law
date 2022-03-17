using BenfordSet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.ViewModel
{
    internal class Clean
    {
        internal void DisposeReadObject(ref ReadPdf? readpdf)
        {
            if (readpdf != null)
            {
                readpdf = null; 
                GC.Collect();
            }
        }
    }
}
