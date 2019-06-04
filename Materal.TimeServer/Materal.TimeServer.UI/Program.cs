using System;
using System.Collections.Generic;
using Materal.DateTimeHelper;

namespace Materal.TimeServer.UI
{
    class Program
    {
        static void Main()
        {
            var timerObservers = new List<ITimerObserver>
            {
                //new TimerTask01(100, DateTimeTypeEnum.Millisecond),
                //new TimerTask02(200, DateTimeTypeEnum.Millisecond),
                //new TimerTask03(DateTime.Now, DateTimeTypeEnum.Second),
                new TimerTask04(DateTime.Now, DateTimeTypeEnum.Minute),
            };
            var timerSubject = new TimerSubject(timerObservers);
            timerSubject.Start();
            while (Console.ReadLine() != "Stop") { }
            timerSubject.Stop();
        }
    }
}
