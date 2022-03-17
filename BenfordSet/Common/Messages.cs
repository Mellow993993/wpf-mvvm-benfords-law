using System;
using System.Windows;

namespace BenfordSet.Common
{
    public class Messages
    {
        public void Validation_ObjectIsNull(object sender, EventArgs e)
        {
            MessageBox.Show("The object is null", "Error",
            MessageBoxButton.OK, MessageBoxImage.Error);
        }
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
