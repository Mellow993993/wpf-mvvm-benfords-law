using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
