using BenfordSet.Model;
using System;

namespace BenfordSet.ViewModel
{
    internal class Clean
    {
        internal void DisposeReadObject(ref ReadPdf? readpdf)
        {
            if(readpdf != null)
            {
                readpdf = null;
                GC.Collect();
            }
        }
    }
}
