using System;
using System.Diagnostics;
using System.Threading;

namespace lock_free
{
    // The test is exploring the performance of full-positive CompareExchange case
    // Need to add multiple contending writers, contending for 'free'(0) positions on a single array
    class PerformanceTest
    {
        static void Main(string[] args)
        {
            var watch = new Stopwatch();
            IterateWithCompareAndSwap(watch);
            watch.Reset();
            IterateWithLock(watch);
            watch.Reset();
            IterateWithPreRead(watch);
        }

        private static void ReportElapsedTime(String method, Stopwatch watch)
        {
            Console.WriteLine("{0} - Timewatch.Elapsed.Ticks: {1}", method, watch.Elapsed.Ticks);
        }

        private static void IterateWithCompareAndSwap(Stopwatch watch)
        {
            Int32[] array = new Int32[Int16.MaxValue];
            watch.Start();
            for (var i = 0; i < array.Length; ++i)
            {
                Interlocked.CompareExchange(ref array[i], 1, 1);
            }
            watch.Stop();
            ReportElapsedTime("IterateWithCompareAndSwap", watch);
        }

        private static void IterateWithPreRead(Stopwatch watch)
        {
            Int32[] array = new Int32[Int16.MaxValue];
            watch.Start();
            for (var i = 0; i < array.Length; ++i)
            {
                if (array[i] == 1)
                    Interlocked.CompareExchange(ref array[i], 1, 1);
            }
            watch.Stop();
            ReportElapsedTime("IterateWithPreRead", watch);
        }

        private static void IterateWithLock(Stopwatch watch)
        {
            Int32[] array = new Int32[Int16.MaxValue];
            watch.Start();
            lock (array)
            {
                for (var i = 0; i < array.Length; ++i)
                {
                    if (array[i] == 1)
                        array[i] = Int16.MaxValue;
                }
            }
            watch.Stop();
            ReportElapsedTime("IterateWithLock", watch);
        }
    }
}
