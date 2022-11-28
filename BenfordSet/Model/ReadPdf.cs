using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
using BenfordSet.Common;

namespace BenfordSet.Model
{
    internal class ReadPdf
    {
        private int _endReadingProcess = 1000 * 240; // abort reading process after 240 seconds

        public int NumberOfPages { get; private set; }
        public string OnlyFileName { get => Path.GetFileName(Filename); }
        internal bool CancelReading { get; set; }
        public string? Content { get; private set; }
        public string Filename { get; private set; }
        private Messages Messages { get; set; }

        public event EventHandler? ReadingAborted;

        public ReadPdf(string filename) 
        {
            (Filename) = (filename);
            Messages = new();
            ReadingAborted += Messages.CancelReading;

        }
        public async Task GetFileContent()
        {
            CancellationTokenSource src = new CancellationTokenSource();
            CancellationToken ct = src.Token;
            ct.Register(() => ReadingAborted?.Invoke(this, EventArgs.Empty));

            Task readfile = Task.Factory.StartNew(() =>
            {
                using PdfDocument document = PdfDocument.Open(Filename);
                {
                    foreach (var page in document.GetPages())
                    {
                        src.CancelAfter(_endReadingProcess);
                        FetchSinglePage(page);

                        if (ct.IsCancellationRequested)
                            return;

                        else if (CancelReading)
                            return;
                    }
                }
            }, ct);
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
