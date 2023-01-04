using BenfordSet.Common;
using System;
using System.Text.RegularExpressions;

namespace BenfordSet.Model
{
    sealed internal class CountNumbers
    {
        #region Properties
        internal int[] FoundNumbers = new int[9];
        internal int NumbersInFile { get; private set; }
        internal ReadPdf ReadPdf { get; private set; }
        internal Messages Messages { get => new Messages(); }
        #endregion

        #region Events
        public delegate void InformUserEventHandler(object source,EventArgs args);
        public event InformUserEventHandler InformUserOnError;
        #endregion

        #region Constructor
        internal CountNumbers(ReadPdf readPdf)
        {
            InformUserOnError += Messages.OnInformUserOnError;
            if(readPdf != null)
                ReadPdf = readPdf;
            else
            {
                OnInformUserOnError();
                throw new ArgumentNullException("Argument Null Exception.","Object readpdf is null");
            }
        }
        #endregion

        #region Public methods
        public void SumUpAllNumbers()
        {
            Regex regex = new(@"[1-9]*[1-9]");
            foreach(Match match in regex.Matches(ReadPdf.Content))
                AssignNumbers(match);
        }
        #endregion

        #region Private methods "AssignNumbers", "OnInformUserOnError"
        private void AssignNumbers(Match match)
        {
            NumbersInFile += 1;
            if(match.Value.StartsWith("1"))
                FoundNumbers[0] += 1;
            else if(match.Value.StartsWith("2"))
                FoundNumbers[1] += 1;
            else if(match.Value.StartsWith("3"))
                FoundNumbers[2] += 1;
            else if(match.Value.StartsWith("4"))
                FoundNumbers[3] += 1;
            else if(match.Value.StartsWith("5"))
                FoundNumbers[4] += 1;
            else if(match.Value.StartsWith("6"))
                FoundNumbers[5] += 1;
            else if(match.Value.StartsWith("7"))
                FoundNumbers[6] += 1;
            else if(match.Value.StartsWith("8"))
                FoundNumbers[7] += 1;
            else if(match.Value.StartsWith("9"))
                FoundNumbers[8] += 1;
            else
                throw new BenfordException() { Information = "Numbers could not be added to FoundNumbers array" }; 
        }
        #endregion

        #region Invoke Events
        private void OnInformUserOnError()
            => InformUserOnError?.Invoke(this,EventArgs.Empty);
        #endregion
    }
}
