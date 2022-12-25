using System;
using System.Windows.Forms;

namespace BenfordSet.Common
{
    internal class Select
    {
        #region Fields
        private string _filepath = string.Empty;
        #endregion

        #region Properties
        internal Messages? Messages => new();
        #endregion

        #region Events
        public event EventHandler FileSelected;
        public event EventHandler NoFileSelected;
        #endregion

        #region Constructor
        internal Select()
        {
            NoFileSelected += Messages.FileHasNotBeenSelected;
            FileSelected += Messages.FileHasBeenSelected;
        }
        #endregion

        #region Public methods
        internal string OpenSelectDialog()
        {
            InitializeWindow();
            if(!string.IsNullOrEmpty(_filepath))
            {
                FileSelected?.Invoke(this,EventArgs.Empty);
                return _filepath;
            }
            else
            {
                NoFileSelected?.Invoke(this,EventArgs.Empty);
                return String.Empty;
            }
        }
        #endregion

        #region Private Methods
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
                _filepath = openfiledialog.FileName;
        }
        #endregion
    }
}
