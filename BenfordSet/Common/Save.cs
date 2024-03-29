﻿using ceTe.DynamicPDF;
using System;
using System.IO;
using System.Windows.Forms;

namespace BenfordSet.Common
{
    sealed internal class Save
    {
        #region Fields
        private readonly string _initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private const string _allowedFilesText = "txt files (*.txt)|*.txt";
        private const string _allowedFilesPdf = "Pdf Documents|*.pdf";
        #endregion

        #region Properties
        internal bool IsText { get; set; }
        internal string Destination { get; set; } = null!;
        internal string OutputResult { get; set; }
        internal Messages Messages => new();
        #endregion

        #region Events
        public event EventHandler? SaveSuccessful;
        public event EventHandler? SaveNotSuccessful;
        #endregion

        #region Constructor
        internal Save(string outputresults,bool istext)
        {
            (OutputResult, IsText) = (outputresults, istext);
            SaveSuccessful += Messages.FileHasBeenSaved;
            SaveNotSuccessful += Messages.FileHasNotBeenSaved;
            OpenSaveDialog();
            SaveFile();       
        }
        #endregion

        #region Private methods
        private void OpenSaveDialog()
        {
            SaveFileDialog saveFileDialog = new()
            {
                InitialDirectory = _initialDirectory, Filter = SetFileExtension()
            };

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
                Destination = saveFileDialog.FileName;
        }

        private string SetFileExtension()
            => IsText ? _allowedFilesText : _allowedFilesPdf;

        private void SaveFile()
        {
            if(IsText && CheckDestination())
            { 
                SaveAsText(); 
                SaveSuccessful?.Invoke(this,new EventArgs());
            }

            else if(!IsText && CheckDestination())
            {
                SaveAsPdf(); 
                SaveSuccessful?.Invoke(this,new EventArgs());
            }

            else
                SaveNotSuccessful?.Invoke(this,new EventArgs());
        }

        private bool CheckDestination()
            => !string.IsNullOrEmpty(Destination);

        private void SaveAsPdf()
        {
            Document doc = new();
            Page page = new(PageSize.A4,PageOrientation.Portrait);
            _ = doc.Pages.Add(page);
            ceTe.DynamicPDF.PageElements.Label label = new
            (OutputResult,0,0,504,800,Font.Helvetica,14,TextAlign.Left);
            page.Elements.Add(label);
            doc.Draw(Destination);
        }

        private void SaveAsText()
        {
            using StreamWriter fs = new(Destination);
            fs.Write(OutputResult);
        }
        #endregion

    }
}
