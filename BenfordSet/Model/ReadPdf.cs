using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;


namespace BenfordSet.Model
{
    internal class FileAttributes
    {
        private string _filename = string.Empty;
        private string _content = string.Empty;
        private int _pageNumbers = 0;
        public string? Filename { get => _filename; set => _filename = value; }
        public int PageNumbers { get => _pageNumbers; set => _pageNumbers = value; }
        public string? Content { get => _content; set => _content = value; }
    }

    internal class ReadPdf : FileAttributes
    {
        public ReadPdf(string filename) { Filename = filename; }
        public void GetFileContent()
        {
            using PdfDocument document = PdfDocument.Open(Filename);
            foreach (var page in document.GetPages())
                FetchSinglePage(page);
        }
        

        private void FetchSinglePage(Page page)
        {
            if (page == null)
                throw new ArgumentNullException();

            Content += page.Text;
            PageNumbers = page.Number;
        }
    }
}
