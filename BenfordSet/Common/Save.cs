using BenfordSet.Model;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace BenfordSet.Common
{
    internal class Save
    {
        #region Fields & properties
        private string _initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //@"C:\";
        private string _allowedFiles = "Text file (*.txt)|*.txt";

        public bool IsPdf { get; set; }
        public string Destination { get; set; }
        public string OutputResult { get; set; }
        #endregion

        public Save(string outputresults, bool ispdf) 
        { 
            OutputResult = outputresults; IsPdf = ispdf;  
        }


        public void OpenSaveDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = _initialDirectory;
            saveFileDialog.Filter = _allowedFiles;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                Destination = saveFileDialog.FileName;
        }

        public void SaveFile()
        {
            if (!IsPdf)
                SaveAsText();
            else
                SaveAsPdf();
        }

        private void SaveAsPdf()
            => throw new NotImplementedException();


        private void SaveAsText()
        {
            //var documentpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter fs = new StreamWriter(Destination))
            {
                fs.Write(OutputResult);
            }
        }
    }
}
