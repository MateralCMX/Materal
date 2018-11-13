using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Materal.NetworkHelper;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        private static readonly Timer _timer = new Timer(10);
        public static void Main()
        {
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
            //Task.Run(async () => { await Init(); });
            Console.ReadKey();
        }

        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Task.WaitAll(InitAsync());
        }

        public static async Task InitAsync()
        {
            try
            {
                int a = 0;
                int b = 1;
                int c = b / a;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
