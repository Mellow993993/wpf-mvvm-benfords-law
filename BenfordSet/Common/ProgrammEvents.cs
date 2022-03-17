using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BenfordSet.Model;


namespace BenfordSet.Common
{
    public class ProgrammEvents
    {
        public event EventHandler ReadingAborted;
        public event EventHandler SaveSuccessful;
        public event EventHandler SaveNotSuccessful;
        public event EventHandler FileSelected;
        public event EventHandler NoFileSelected;
        public event EventHandler CheckRequired;
        public event EventHandler NoCheckRequired;
        public event EventHandler IsCanceld;

        //public event EventHandler? CheckFileRequired;
        //public event EventHandler? NoCheckFileRequired;

        public ProgrammEvents()
        {
            var calculation = new Calculation();
            //calculation.CheckFileRequired += CheckFileRequired;
            FileSelected += FileHasBeenSelected;
        }

        public void OnCancel()
            => IsCanceld?.Invoke(this, EventArgs.Empty);
        public void OnSaveNotSuccessful()
            => SaveNotSuccessful.Invoke(this, EventArgs.Empty);
        public void OnReadingAborted()
            => ReadingAborted?.Invoke(this, EventArgs.Empty);
        public void OnSaveSuccessful()
            => SaveSuccessful?.Invoke(this, EventArgs.Empty);
        public void OnFileSelected()
            => FileSelected?.Invoke(this, EventArgs.Empty);
        public void OnNoFileSelected()
            => NoFileSelected?.Invoke(this, EventArgs.Empty);
        public void OnCheckRequired()
            => CheckRequired?.Invoke(this, EventArgs.Empty);
        public void OnNoCheckRequred()
            => NoCheckRequired?.Invoke(this, EventArgs.Empty);

        public void CancelProcess(object sender, EventArgs e)
        {
            MessageBox.Show("The process has been cancled", "Info",
            MessageBoxButton.OK, MessageBoxImage.Stop);
        }

        public void NoCheckFileRequred(object sender, EventArgs e)
        {
            MessageBox.Show("No issues detected", "Info",
            MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void CheckFileRequired(object sender, EventArgs e)
        {
            MessageBox.Show("You should check that file", "Warning",
            MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public void FileHasBeenSelected(object sender, EventArgs e)
        {
            MessageBox.Show("PDF file has been selected", "Info",
            MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void FileHasNotBeenSelected(object sender, EventArgs e)
        {
            MessageBox.Show("No PDF file has been selected", "Warning",
            MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void FileHasBeenSaved(object sender, EventArgs e)
        {
            MessageBox.Show("File has been saved successfully.", "Info",
            MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void FileHasNotBeenSaved(object sender, EventArgs e)
        {
            MessageBox.Show("File has not been saved.", "Info",
            MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public void CancelReading(object sender, EventArgs e)
        {
            MessageBox.Show("Reading process has been aborted.", "Fehler",
            MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

   
}
