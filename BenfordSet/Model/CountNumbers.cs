using System;
using System.Text.RegularExpressions;

namespace BenfordSet.Model
{
    internal class CountNumbers
    {
        public int[] FoundNumbers = new int[9];
        public int NumbersInFile { get; private set; }
        public ReadPdf ReadPdf { get; private set; }
        public CountNumbers(ReadPdf readPdf)
        {
            ReadPdf = readPdf;
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
                throw new ArgumentException("Could not parsed");
            }
        }
    }
}
