using System.Diagnostics;

namespace BenfordSet.Common
{
    internal class Timing
    {
        public Stopwatch Stopwatch { get; set; }    
        public void StartTimeMeasurement()
        {
            Stopwatch.Start();
        }    
        
        public void StopTimeMeasurement()
        {
            Stopwatch.Stop();
        }
    }
}
