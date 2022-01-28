using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace BenfordSet.Model
{
    internal class CountNumbers : FileAttributes
    {
        private int _numberInFile = 0;
        public int NumbersInFile { get => _numberInFile; set => _numberInFile = value; }
        public int[] FoundNumbers { get; set; } = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public CountNumbers(ReadPdf file) { Content = file.Content; } //FileContent = readFile.FileContent;


        public void SumAllNumbersUp()
        {
            Regex regex = new Regex(@"[1-9]*[1-9]"); 
            foreach (Match match in regex.Matches(Content))
                AssignNumbers(match);
        }


        private void AssignNumbers(Match match)
        {

            NumbersInFile += 1;
            if (match.Value.StartsWith("1"))
                FoundNumbers[0] += 1;

            else if (match.Value.StartsWith("2"))
                FoundNumbers[1] += 1;

            else if (match.Value.StartsWith("3"))
                FoundNumbers[2] += 1;

            else if (match.Value.StartsWith("4"))
                FoundNumbers[3] += 1;

            else if (match.Value.StartsWith("5"))
                FoundNumbers[4] += 1;

            else if (match.Value.StartsWith("6"))
                FoundNumbers[5] += 1;

            else if (match.Value.StartsWith("7"))
                FoundNumbers[6] += 1;

            else if (match.Value.StartsWith("8"))
                FoundNumbers[7] += 1;

            else if (match.Value.StartsWith("9"))
                FoundNumbers[8] += 1;
            else
                throw new ArgumentException("Could not parsed");
        }

        public void ShowAllNumbers()
        {
            MessageBox.Show(NumbersInFile.ToString());
        }
    }
}
