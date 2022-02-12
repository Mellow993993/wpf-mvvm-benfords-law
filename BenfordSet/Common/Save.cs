using BenfordSet.Model;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;


namespace BenfordSet.Common
{
    internal class Save
    {
        private string _initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private string _allowedFiles = "Text file (*.txt)|*.txt";

        private ProgrammEvents _programmEvents;
        public ProgrammEvents ProgrammEvents { get => _programmEvents; set => _programmEvents = value; }

        public bool IsText { get; set; }
        public string Destination { get; set; }
        public string OutputResult { get; set; }

        public Save(string outputresults, bool istext)
        { 
            OutputResult = outputresults; IsText = istext;
            _programmEvents = new ProgrammEvents();
            _programmEvents.SaveSuccessful += SaveSuccessful;
            _programmEvents.SaveNotSuccessful += SaveNotSuccessful;
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
            {
                SaveAsText();
                _programmEvents.OnSaveSuccessful();
            }
            else
            {
                SaveAsPdf();
                _programmEvents.OnSaveNotSuccessful();
                _programmEvents.OnSaveSuccessful();
            }
        }

        private void SaveAsPdf() 
        {
            System.Windows.MessageBox.Show("print as pdf");

        }


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


        private void SaveSuccessful(object sender, EventArgs e)
        {
            System.Windows.MessageBox.Show("File has been saved successfully.", 
                "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveNotSuccessful(object sender, EventArgs e)
        {
            System.Windows.MessageBox.Show("File has not been saved.",
                  "Info", MessageBoxButton.OK, MessageBoxImage.Warning);

        }
    }
}
