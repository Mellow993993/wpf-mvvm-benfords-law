using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenfordSet.Logging
{
    public class FileLogger : ILog
    {
        private readonly List<string> _logMessage;

        public FileLogger()
        {
            _logMessage = new List<string>();
        }

        public void Log(string message)
        {
            _logMessage.Add(DateTime.Now.ToString(CultureInfo.CurrentCulture) + "   " + _logMessage);
        }

        public string WriteToFile()
        {
            string pathToLogFile = Path.Combine(Path.GetTempPath(),
                "MetroBackUp " + DateTime.Now.ToString("dd.MM.yyyy hh-mm-ss", CultureInfo.CurrentCulture) + ".log");

            try
            {
                using (var streamWriter = new StreamWriter(pathToLogFile, false, Encoding.UTF8))
                {
                    foreach (string message in _logMessage)
                    {
                        streamWriter.WriteLine(message);
                    }

                    streamWriter.Flush();
                }

                return pathToLogFile;
            }
            catch (IOException)
            {
                _logMessage.Clear();
                return null;
            }
            finally
            {
                _logMessage.Clear();
            }
        }
    }
}
