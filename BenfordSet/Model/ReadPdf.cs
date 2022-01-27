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
    internal class ReadPdf
    {
        public string Filename { get; set; }
        public int PageNumber { get; set; }
        public string Content { get; set; }
        public ReadPdf(string filename)
        {
            Filename = filename;
        }

        public void GetFileContent()
        {
            using PdfDocument document = PdfDocument.Open(Filename);
            foreach (var page in document.GetPages())
            {
                //Content += p.Text;
                FetchSinglePage(page); // += readFilesCompleted;
            }
            //Task.WaitAll(GetFileContent());
        }

        private void FetchSinglePage(Page p)
        {
            if (p == null)
                throw new ArgumentNullException();

            Content += p.Text;
            PageNumber += p.Number;
        }
    }
}
