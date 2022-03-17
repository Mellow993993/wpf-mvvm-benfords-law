using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System;
using BenfordSet.Common;

namespace BenfordSet.Model
{
    internal class FileAttributes
    {
        private string _filename = string.Empty;
        private string _content = string.Empty;
        private int _NumberOfPages = 0;

        public string? Filename { get => _filename; set => _filename = value; }
        public int NumberOfPages { get => _NumberOfPages; set => _NumberOfPages = value; }
        public string? Content { get => _content; set => _content = value; }
        public string OnlyFileName { get => Path.GetFileName(Filename);  }
    }

    internal class ReadPdf 
    {
        private string _filename;
        private int _endReadingProcess = 1000 * 240; // abort reading process after 240 seconds

        public int NumberOfPages { get; private set; }
        public string OnlyFileName { get => Path.GetFileName(_filename); private set => Path.GetFileName(_filename); }
        internal bool CancelReading { get; set; }
        public string? Content { get; private set; }
        private Messages Messages { get; set; }

        public event EventHandler? ReadingAborted;

        public ReadPdf(string filename) 
        {
            _filename = filename;
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
                using PdfDocument document = PdfDocument.Open(_filename);
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
