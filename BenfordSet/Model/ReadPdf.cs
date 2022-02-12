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
        private int _endReadingProcess = 1000 * 120; // abort reading process after 120 seconds
        private ProgrammEvents _programmEvents;

        public ProgrammEvents ProgrammEvents { get => _programmEvents; set => _programmEvents = value; }
        public ReadPdf(string filename) 
        { 
            Filename = filename;
            _programmEvents = new ProgrammEvents();
            _programmEvents.ReadingAborted += CancelReading;
        }

        public async Task GetFileContent()
        {
            CancellationTokenSource src = new CancellationTokenSource();
            CancellationToken ct = src.Token;
            ct.Register(() => ProgrammEvents.OnReadingAborted());

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
                    }
                 }
            }, ct);
            await readfile;
        }


        private void FetchSinglePage(Page page)
        {
            Content += page.Text;
            NumberOfPages = page.Number;
        }

        private void CancelReading(object sender, EventArgs e)
        {
            MessageBox.Show("Reading process has been aborted.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }


    }
}
