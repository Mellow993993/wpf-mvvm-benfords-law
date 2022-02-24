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

    internal class ReadPdf : FileAttributes
    {
        private int _endReadingProcess = 1000 * 240; // abort reading process after 240 seconds
        private ProgrammEvents _events;
        private bool _cancelReading;
        public bool CancelReading
        {
            get => _cancelReading;
            set => _cancelReading = value; 
        }
        public ReadPdf(string filename) 
        { 
            Filename = filename;
            _events = new ProgrammEvents();
            _events.ReadingAborted += _events.CancelReading;
        }

        public async Task GetFileContent()
        {
            CancellationTokenSource src = new CancellationTokenSource();
            CancellationToken ct = src.Token;

            ct.Register(() => _events.OnReadingAborted());

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
            _events.ReadingAborted -= _events.CancelReading;
            await readfile;
        }


        private void FetchSinglePage(Page page)
        {
            Content += page.Text;
            NumberOfPages = page.Number;
        }
    }
}
