using BenfordSet.Model;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace BenfordSet.Common
{
    internal class Save
    {
        private string _initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private string _allowedFiles = "Text file (*.txt)|*.txt";

        public bool IsText { get; set; }
        public string Destination { get; set; }
        public string OutputResult { get; set; }

        public Save(string outputresults, bool istext) 
        { 
            OutputResult = outputresults; IsText = istext;  
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
            if (IsText)
                SaveAsText();
            else
                SaveAsPdf();
        }

        private void SaveAsPdf()
            => MessageBox.Show("print as pdf");


        private void SaveAsText()
        {
            if(!string.IsNullOrEmpty(Destination))
            {
                using (StreamWriter fs = new StreamWriter(Destination))
                {
                    fs.Write(OutputResult);
                }
            }
        }
    }
}
