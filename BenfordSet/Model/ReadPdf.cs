using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using System.IO;
using System.Threading.Tasks;

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
        public ReadPdf(string filename) { Filename = filename; }
        //public void GetFileContent()
        //{
        //    using PdfDocument document = PdfDocument.Open(Filename);
        //    {
        //        foreach (var page in document.GetPages())
        //            FetchSinglePage(page);
        //    }
        //}


        public async Task<string> GetFileContent()
        {
            Task<string> readfile = Task<string>.Factory.StartNew(() =>
            {
                using PdfDocument document = PdfDocument.Open(Filename);
                {
                    foreach (var page in document.GetPages())
                        FetchSinglePage(page);
                }
                return Content;
            });
            var t = await readfile;
            return t;
        }


        private void FetchSinglePage(Page page)
        {
            Content += page.Text;
            NumberOfPages = page.Number;
        }
    }
}
