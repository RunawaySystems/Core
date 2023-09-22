using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RunawaySystems {
    public static class StopWatchExtensions {

        /// <summary>
        /// If platform supports high resolution performance counters (<see cref="Stopwatch.IsHighResolution"/>), you'll receive milliseconds with a fractional component. <br/>
        /// Otherwise you'll receive elapsed time in whole milliseconds.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ElapsedMillisecondsHighResolution(this Stopwatch stopwatch) {
            return ((double)stopwatch.ElapsedTicks / Stopwatch.Frequency) * 1000d;
        }
    }
}
