using BenfordSet.Common;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace BenfordSet.Model
{
    internal class ReadPdf
    {
        private readonly int _endReadingProcess = 1000 * 300; // abort reading process after 5 minutes
        public int NumberOfPages { get; private set; }
        public string OnlyFileName => Path.GetFileName(Filename);
        internal bool CancelReading { get; set; }
        public string? Content { get; private set; }
        public string Filename { get; private set; }
        private Messages Messages { get; set; }

        public event EventHandler? ReadingAborted;

        public ReadPdf(string filename)
        {
            if(!string.IsNullOrWhiteSpace(filename))
            {
                Filename = filename;
                Messages = new();
                ReadingAborted += Messages.CancelReading;
            }
            else
                throw new BenfordException() { Information = "The filename is empty or whitespace" };

        }
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

        private void FetchSinglePage(Page page)
        {
            Content += page.Text;
            NumberOfPages = page.Number;
        }

    }
}
