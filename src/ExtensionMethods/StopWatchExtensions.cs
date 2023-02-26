using System.Diagnostics;

namespace RunawaySystems {
    public static class StopWatchExtensions {

        /// <summary>
        /// If platform suppors high resolution performance counters (<see cref="Stopwatch.IsHighResolution"/>), you'll receive milliseconds with a fractional component. <br/>
        /// Otherwise you'll receive elapsed time in whole milliseconds.
        /// </summary>
        public static double ElapsedMillisecondsHighResolution(this Stopwatch stopwatch) {
            return ((double)stopwatch.ElapsedTicks / Stopwatch.Frequency) * 1000d;
        }
    }
}
