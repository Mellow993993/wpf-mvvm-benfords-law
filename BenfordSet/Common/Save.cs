using BenfordSet.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenfordSet.Common
{
    internal class Save
    {
        #region Fields & properties
        private string _initialDirectory = @"C:\";
        private string _allowedFiles = "Text file (*.txt)|*.txt";

        public Results Results { get; set; }
        private string Destination { get; set; }
        public StringBuilder Content { get; set; }

        #endregion

        #region Constructor
        public Save() { }
        public Save(Results results, string destination)
        {
            Results = results;
            Destination = destination;
        }
        #endregion

        #region Public methods (SaveFile)

        public void OpenSaveDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = _initialDirectory;
            saveFileDialog.Filter = _allowedFiles;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                _ = saveFileDialog.FileName;
        }

        public bool SaveFile()
        {
            DirectoryInfo directoryinfo = new DirectoryInfo(Path.GetDirectoryName(Destination));
            if (directoryinfo.Exists)
            {
                PrepareOutput();
                return true;
            }
            else
                return false;
        }
        #endregion

        #region Private methods (Stringbuilder)
        private void PrepareOutput() => File.AppendAllText(Destination, BuildInformation());
        private string BuildInformation()
        {
            StringBuilder Content = new StringBuilder();
            Content.Append("hallo welt");
            //_ = Content.Append("\nAktueller Tag\t" + Results + "\n");

            //("Threshold:\t {0} %", calcObj.Threshold);
            //("Filename:\t {0}", calcObj.Filename);
            //("Counted Numbers: {0}", calcObj.NumbersInFile);
            //("\nBenford Distribution \t Your Distribution \t Difference ");

            //for (var i = 0; i < calcObj.CountedNumbers.Length; i++)
            //{
            //    if (calcObj.Difference[i] < calcObj.Threshold)
            //    {
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        Console.WriteLine("{0}: {1} % \t\t {2}: {3} %  \t\t {4}: {5} %", i + 1, calcObj.BenfordNumbers[i], i + 1, calcObj.Digits[i], i + 1, calcObj.Difference[i]);
            //    }
            //    else
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine("{0}: {1} % \t\t {2}: {3} %  \t\t {4}: {5} %", i + 1, calcObj.BenfordNumbers[i], i + 1, calcObj.Digits[i], i + 1, calcObj.Difference[i]);
            //    }
            //}
            //Console.ForegroundColor = ConsoleColor.Gray;
            //PrintDeviation(calcObj.CountDeviations);

            return Content.ToString();
        }
        #endregion
    }
}
