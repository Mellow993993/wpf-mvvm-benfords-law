﻿using BenfordSet.Common;
using Microsoft.VisualBasic;
using System;
using System.Text;

namespace BenfordSet.Model
{
    internal class Results
    {
        #region Properties
        public string TotalTime { get; private set; }
        public ReadPdf ReadPdf { get; private set; }
        public Calculation Calculation { get; private set; }
        public CountNumbers CountNumbers { get; private set; }
        public Messages Messages { get => new Messages(); }
        #endregion

        #region Events
        public delegate void InformUserEventHandler(object source,EventArgs args);
        public event InformUserEventHandler InformUserOnError;
        #endregion

        #region Constructor 
        internal Results(ReadPdf read,CountNumbers count,Calculation calculation,string totaltime)
        {
            InformUserOnError += Messages.OnInformUserOnError;
            if(read != null && count != null && calculation != null)
                (ReadPdf, CountNumbers, Calculation, TotalTime) = (read, count, calculation, totaltime);
            else
            {
                OnInformUserOnError();
                throw new BenfordException() { Information = "At least one of the ctor objects is null" };
            }
        }
        #endregion

        #region Public methods
        public string BuildResultHeader()
            => PrintHeadLine() + PrintMetaInfos();
        #endregion

        #region Private methods "OnInformUserOnError", "PrintHeadLine", "PrintMetaInfos"
        private void OnInformUserOnError()
        {
            if(InformUserOnError != null)
                InformUserOnError(this,EventArgs.Empty);
        }
        private string PrintHeadLine()
        {
            StringBuilder sb = new();
            _ = sb.AppendLine("Results of the Benford analysis.");
            return sb.ToString();
        }
        private string PrintMetaInfos()
        {
            StringBuilder sb = new();
            _ = sb.AppendLine($"Filename:   {ReadPdf.OnlyFileName}");
            _ = sb.AppendLine($"Pages:      {ReadPdf.NumberOfPages}");
            _ = sb.AppendLine($"Numbers:    {CountNumbers.NumbersInFile}");
            _ = sb.AppendLine($"Issues:     {Calculation.CountDeviations}");
            _ = sb.AppendLine($"Threshold:  {Calculation.Threshold} %");
            _ = sb.AppendLine($"Time:       {TotalTime} ms");
            _ = sb.AppendLine("------------------------------------------------------------");
            _ = sb.AppendLine("Distribution");

            _ = sb.AppendLine("Benford\t\tYours\t\t\tDifference");
            return sb.ToString();
        }
        #endregion
    }
}
