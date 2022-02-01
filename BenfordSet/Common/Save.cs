using BenfordSet.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig;


namespace BenfordSet.Common
{
    internal class Save
    {
        #region Fields & properties
        private string _initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //@"C:\";
        private string _allowedFiles = "Text file (*.txt)|*.txt";

        public Results Results { get; set; }
        private string Destination { get; set; }
        public StringBuilder Content { get; set; }

        public string OutputResult { get; set; }

        #endregion

        #region Constructor
        public Save(string outputresults) { OutputResult = outputresults; }

        public Save() { }
        public Save(Results results, string destination)
        {
            Results = results;
            Destination = destination;
        }
        #endregion

        #region Public methods (SaveFile)

        public void OpenSaveDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = _initialDirectory;
            saveFileDialog.Filter = _allowedFiles;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                _ = saveFileDialog.FileName;
        }

        public bool SaveFile()
        {

            //if (!String.IsNullOrEmpty(saveFileDialog.FileName))
            //{
            //    Destination = saveFileDialog.FileName;
            //    UserSettings su = new UserSettings(saveFileDialog.FileName);
            //    su.SetRegistry();
            //    if (saveTimeKeeping.SaveFile())
            //    {
            //        RaiseSave();
            //        OnPropertyChanged(nameof(Destination));
            //    }
            //}
            //else
            //    RaiseNoSave();




            DirectoryInfo directoryinfo = new DirectoryInfo(Path.GetDirectoryName(Destination));
            if (directoryinfo.Exists)
            {
                PrepareOutput();
                return true;
            }
            else
                return false;
        }
        #endregion

        #region Private methods (Stringbuilder)
        private void PrepareOutput() => File.AppendAllText(Destination, BuildInformation());
        private string BuildInformation()
        {
            StringBuilder Content = new StringBuilder();
            Content.Append("hallo welt");
            return Content.ToString();
        }
        #endregion


        private void SaveAsPdf()
        { 
        
        }

        public void SaveAsText()
        {
            var documentpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter fs = new StreamWriter(Path.Combine(documentpath, "BenfordAnalyse.txt")))
            {
                fs.Write(OutputResult);
            }
        }
    }
}
