using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ParallelTest
{
    public class Program
    {
        private static readonly Stopwatch stopWatch = new Stopwatch();
        public static void Main(string[] args)
        {
            ParallelBreak();
            Console.ReadKey();
        }
        public static void ParallelForMethod()
        {
            stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                var sum = 0;
                for (var j = 0; j < 60000; j++)
                {
                    sum += i;
                }
            }
            stopWatch.Stop();
            Console.WriteLine("NormalFor run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Reset();
            stopWatch.Start();
            Parallel.For(0, 10000, item =>
            {
                var sum = 0;
                for (var j = 0; j < 60000; j++)
                {
                    sum += item;
                }
            });
            stopWatch.Stop();
            Console.WriteLine("ParallelFor run " + stopWatch.ElapsedMilliseconds + " ms.");
        }
        public static void ParallelBreak()
        {
            var bag = new ConcurrentBag<int>();
            stopWatch.Start();
            Parallel.For(0, 1000, (i, state) =>
            {
                if (bag.Count == 300)
                {
                    state.Stop();
                    return;
                }
                bag.Add(i);
            });
            stopWatch.Stop();
            Console.WriteLine("Bag count is " + bag.Count + ", " + stopWatch.ElapsedMilliseconds);
        }
    }
}
