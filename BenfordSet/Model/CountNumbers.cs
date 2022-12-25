﻿using BenfordSet.Common;
using System;
using System.Text.RegularExpressions;

namespace BenfordSet.Model
{
    internal class CountNumbers
    {
        public int[] FoundNumbers = new int[9];
        public int NumbersInFile { get; private set; }
        public ReadPdf ReadPdf { get; private set; }

        public delegate void InformUserEventHandler(object source,EventArgs args);
        public event InformUserEventHandler InformUser;

        public CountNumbers(ReadPdf readPdf)
        {
            Messages message = new Messages();
            readPdf = null;
            message.
            if(readPdf != null)
                ReadPdf = readPdf;
            else
            {
                OnInformUser();
                throw new ArgumentNullException("Argument Null Exception.","Object readpdf is null");
            }
        }

        private void OnInformUser()
        {
            if(InformUser != null)
            {
                InformUser(this,EventArgs.Empty);
            }
        }

        public void SumUpAllNumbers()
        {
            Regex regex = new(@"[1-9]*[1-9]");

            foreach(Match match in regex.Matches(ReadPdf.Content))
            {
                AssignNumbers(match);
            }
        }

        private void AssignNumbers(Match match)
        {
            NumbersInFile += 1;

            if(match.Value.StartsWith("1"))
            {
                FoundNumbers[0] += 1;
            }
            else if(match.Value.StartsWith("2"))
            {
                FoundNumbers[1] += 1;
            }
            else if(match.Value.StartsWith("3"))
            {
                FoundNumbers[2] += 1;
            }
            else if(match.Value.StartsWith("4"))
            {
                FoundNumbers[3] += 1;
            }
            else if(match.Value.StartsWith("5"))
            {
                FoundNumbers[4] += 1;
            }
            else if(match.Value.StartsWith("6"))
            {
                FoundNumbers[5] += 1;
            }
            else if(match.Value.StartsWith("7"))
            {
                FoundNumbers[6] += 1;
            }
            else if(match.Value.StartsWith("8"))
            {
                FoundNumbers[7] += 1;
            }
            else if(match.Value.StartsWith("9"))
            {
                FoundNumbers[8] += 1;
            }
            else
            {
                throw new BenfordException() { Information = "Numbers could not be added to FoundNumbers array" }; 
            }
        }
    }
}
