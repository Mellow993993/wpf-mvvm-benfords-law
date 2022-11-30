using System.Diagnostics;

namespace BenfordSet.Common
{
    internal class Timing
    {
        public Stopwatch Stopwatch { get; private set; }
        public Timing(Stopwatch sw)
        {
            Stopwatch = sw;
        }

        public void StartTimeMeasurement()
        {
            Stopwatch.Start();
        }

        public string StopTimeMeasurement()
        {
            Stopwatch.Stop();
            return Stopwatch.ElapsedMilliseconds.ToString();
        }
    }
}
