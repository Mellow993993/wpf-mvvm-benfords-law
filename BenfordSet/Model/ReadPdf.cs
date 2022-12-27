using BenfordSet.Common;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace BenfordSet.Model
{
    sealed internal class ReadPdf 
    {
        #region Fields
        private readonly int _endReadingProcess = 1000 * 300; // abort reading process after 5 minutes
        #endregion

        #region Properties
        internal int NumberOfPages { get; private set; }
        internal string OnlyFileName => Path.GetFileName(Filename);
        internal bool CancelReading { get; set; }
        internal string? Content { get; private set; }
        internal string Filename { get; private set; }
        internal Messages Messages { get => new Messages(); }
        #endregion

        #region Events
        public delegate void InformUserEventHandler(object source,EventArgs args);
        public event InformUserEventHandler InformUserOnError;
        public event InformUserEventHandler InformUserOnTimeExpired;
        public event EventHandler? ReadingAborted;
        #endregion

        #region Constructor
        internal ReadPdf(string filename)
        {
            if(!string.IsNullOrWhiteSpace(filename))
            {
                Filename = filename;
                ReadingAborted += Messages.CancelReading;
            }
            else
            {
                OnInformUserOnError();
                throw new BenfordException() { Information = "The filename is empty or whitespace" };
            }
        }
        #endregion

        #region Public methods "GetFileContent"
        public async Task GetFileContent()
        {
            CancellationTokenSource src = new();
            CancellationToken ct = src.Token;
            _ = ct.Register(() => ReadingAborted?.Invoke(this,EventArgs.Empty));

            Task readfile = Task.Factory.StartNew(() =>
            {
                using PdfDocument document = PdfDocument.Open(Filename);
                {
                    foreach(Page? page in document.GetPages())
                    {
                        src.CancelAfter(_endReadingProcess);
                        FetchSinglePage(page);

                        if(ct.IsCancellationRequested)
                        {
                            return;
                        }
                        else if(CancelReading)
                        {
                            return;
                        }
                    }
                }
            },ct);
            await readfile;
            ReadingAborted -= Messages.CancelReading;
        }
        #endregion

        #region Private methods "FetchSinglePage", "OnInformUserOnError"S
        private void FetchSinglePage(Page page)
        {
            Content += page.Text;
            NumberOfPages = page.Number;
        }
        private void OnInformUserOnError()
        {
            if(InformUserOnError != null)
                InformUserOnError(this,EventArgs.Empty);
        }
        #endregion
    }
}
