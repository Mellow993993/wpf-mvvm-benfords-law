using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenfordSet.Common
{
    internal class Select
    {
        private string _filepath = String.Empty;
        public string OpenDialog()
        {
            InitializeWindow();
            if (!String.IsNullOrWhiteSpace(_filepath))
                return _filepath;
            else
                return String.Empty;
        }

        private void InitializeWindow()
        {
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Filter = "pdf files (*.pdf)|*.pdf";
                openFileDialog.Title = "Select a file";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                ShowDialog(openFileDialog);
            }
        }

        private void ShowDialog(System.Windows.Forms.OpenFileDialog openfiledialog)
        {
            if (openfiledialog.ShowDialog() == DialogResult.OK)
                _filepath = openfiledialog.FileName;
        }
    }
}
