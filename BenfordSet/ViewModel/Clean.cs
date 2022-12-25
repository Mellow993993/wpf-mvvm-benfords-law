using BenfordSet.Model;
using System;

namespace BenfordSet.ViewModel
{
    internal class Clean : IDisposable
    {
        #region Fields
        private bool disposed;
        #endregion

        #region Methods
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposed)
                return;
            else
                disposed = true;
        }

        internal void DisposeReadObject(ref ReadPdf? readpdf)
        {
            if(readpdf != null)
            {
                readpdf = null;
                GC.Collect();
            }
        }
        #endregion
    }
}
