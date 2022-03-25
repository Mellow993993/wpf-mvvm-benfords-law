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
        internal Clean(ref ReadPdf read,ref CountNumbers count, ref Calculation calc,ref Results result)
        {
            read = null;
            count = null;
            calc = null;
            result = null;
            GC.Collect();
        }

        internal Clean(ref ReadPdf read)
        {
            read = null;
            GC.Collect();
        }
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
