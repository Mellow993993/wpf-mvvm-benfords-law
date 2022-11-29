﻿using System;
using System.Windows.Forms;

namespace BenfordSet.Common
{
    internal class Select
    {
        private string _filepath = string.Empty;
        public string OpenDialog()
        {
            InitializeWindow();
            if (!string.IsNullOrWhiteSpace(_filepath))
                return _filepath;
            else
                return string.Empty;
        }

        private void InitializeWindow()
        {
            using System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "pdf files (*.pdf)|*.pdf";
            openFileDialog.Title = "Select a file";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            ShowDialog(openFileDialog);
        }

        private void ShowDialog(System.Windows.Forms.OpenFileDialog openfiledialog)
        {
            if (openfiledialog.ShowDialog() == DialogResult.OK)
                _filepath = openfiledialog.FileName;
        }
    }
}
