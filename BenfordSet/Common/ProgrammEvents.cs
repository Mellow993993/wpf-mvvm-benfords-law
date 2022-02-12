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
        public event EventHandler ProcessCompleted;
        public event EventHandler ProcessFailed;
        public event EventHandler ProcessTerminated;
        public event EventHandler ProcessStarted;
        public event EventHandler ProcessStopped;
        public event EventHandler ProcessExited;
        public event EventHandler ReadingAborted;
        public event EventHandler SaveSuccessful;
        public event EventHandler SaveNotSuccessful;

        public void OnSaveNotSuccessful()
        {
            SaveNotSuccessful.Invoke(this, EventArgs.Empty);
        }

        public void OnReadingAborted()
            => ReadingAborted?.Invoke(this, EventArgs.Empty);

        public void OnSaveSuccessful()
            => SaveSuccessful?.Invoke(this, EventArgs.Empty);

        //public void CancelReading(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Reading process has been aborted.", "Fehler", 
        //        MessageBoxButton.OK, MessageBoxImage.Error);
        //}
    }

   
}
