using System;
using System.Windows;

namespace BenfordSet.Common
{
    internal class Messages
    {
        internal void OnInformUserOnError(object sender, EventArgs e)
        {
            _ = MessageBox.Show("An error has occured.\n" +
                "The program will be terminated\nPlease send us an failure build to:" +
                "\nstay.mellow.993@gmail.com","Warning",
            MessageBoxButton.OK,MessageBoxImage.Stop);
        }
        internal void CancelProcess(object sender,EventArgs e)
        {
            _ = MessageBox.Show("The process has been canceled","Info",
            MessageBoxButton.OK,MessageBoxImage.Stop);
        }
        internal void NoCheckFileRequred(object sender,EventArgs e)
        {
            _ = MessageBox.Show("No issues detected","Info",
            MessageBoxButton.OK,MessageBoxImage.Information);
        }
        internal void CheckFileRequired(object sender,EventArgs e)
        {
            _ = MessageBox.Show("You should check that file","Warning",
            MessageBoxButton.OK,MessageBoxImage.Exclamation);
        }
        internal void FileHasBeenSelected(object sender,EventArgs e)
        {
            _ = MessageBox.Show("PDF file has been selected","Info",
            MessageBoxButton.OK,MessageBoxImage.Information);
        }
        internal void FileHasNotBeenSelected(object sender,EventArgs e)
        {
            _ = MessageBox.Show("No PDF file has been selected","Warning",
            MessageBoxButton.OK,MessageBoxImage.Warning);
        }
        internal void FileHasBeenSaved(object sender,EventArgs e)
        {
            _ = MessageBox.Show("File has been saved successfully.","Info",
            MessageBoxButton.OK,MessageBoxImage.Information);
        }
        internal void FileHasNotBeenSaved(object sender,EventArgs e)
        {
            _ = MessageBox.Show("File has not been saved.","Info",
            MessageBoxButton.OK,MessageBoxImage.Warning);
        }
        internal void CancelReading(object sender,EventArgs e)
        {
            _ = MessageBox.Show("Reading process has been aborted.","Fehler",
            MessageBoxButton.OK,MessageBoxImage.Error);
        }
    }
}
