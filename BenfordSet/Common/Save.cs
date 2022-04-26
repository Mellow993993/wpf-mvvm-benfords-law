using BenfordSet.Model;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.Text;
using ceTe.DynamicPDF.Forms;
using ceTe.DynamicPDF.PageElements;
using static System.Net.Mime.MediaTypeNames;


namespace BenfordSet.Common
{
    internal class Save
    {
        public event EventHandler? SaveSuccessful;
        public event EventHandler? SaveNotSuccessful;

        private readonly string _initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private const string _allowedFilesText = "txt files (*.txt)|*.txt";
        private const string _allowedFilesPdf = "Pdf Documents|*.pdf";

        public bool IsText { get; set; }
        public string Destination { get; set; } = null!;
        public string OutputResult { get; set; }
        public Messages Messages { get => new Messages(); }

        public Save(string outputresults, bool istext)
        {  
            (OutputResult, IsText) = (outputresults, istext);
            SaveSuccessful += Messages.FileHasBeenSaved;
            SaveNotSuccessful += Messages.FileHasNotBeenSaved;
        }       

        public void OpenSaveDialog()
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.InitialDirectory = _initialDirectory;
            saveFileDialog.Filter = SetFileExtension(); 

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                Destination = saveFileDialog.FileName;
        }

        private string SetFileExtension()
            => IsText ? _allowedFilesText : _allowedFilesPdf;

        public void SaveFile()
        {
            if(IsText && CheckDestination())
            {
                SaveAsText();
                SaveSuccessful?.Invoke(this, new EventArgs());
            }

            else if(!IsText && CheckDestination())
            {
                SaveAsPdf();
                SaveSuccessful?.Invoke(this, new EventArgs());
            }

            else
                SaveNotSuccessful?.Invoke(this, new EventArgs());
        }

        private bool CheckDestination() => !string.IsNullOrEmpty(Destination);

        private void SaveAsPdf() 
        {
            Document doc = new();
            Page page = new Page(PageSize.A4, PageOrientation.Portrait);
            doc.Pages.Add(page);
            ceTe.DynamicPDF.PageElements.Label label = new
            ceTe.DynamicPDF.PageElements.Label(OutputResult, 0, 0, 504, 800, Font.Helvetica, 14, TextAlign.Left);
            page.Elements.Add(label);
            doc.Draw(Destination);
        }

        private void SaveAsText()
        {
            using StreamWriter fs = new(Destination);
            fs.Write(OutputResult);
        }
    }
}
