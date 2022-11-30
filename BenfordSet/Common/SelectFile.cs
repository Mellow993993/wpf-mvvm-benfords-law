using System;
using System.Windows.Forms;

namespace BenfordSet.Common
{
    internal class Select
    {
        private string _filepath = string.Empty;
        public string OpenDialog()
        {
            InitializeWindow();
            return !string.IsNullOrWhiteSpace(_filepath) ? _filepath : string.Empty;
        }

        private void InitializeWindow()
        {
            using System.Windows.Forms.OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "pdf files (*.pdf)|*.pdf";
            openFileDialog.Title = "Select a file";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            ShowDialog(openFileDialog);
        }

        private void ShowDialog(System.Windows.Forms.OpenFileDialog openfiledialog)
        {
            if(openfiledialog.ShowDialog() == DialogResult.OK)
            {
                _filepath = openfiledialog.FileName;
            }
        }
    }
}
