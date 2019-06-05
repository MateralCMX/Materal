using Materal.DateTimeHelper;
using System;

namespace Materal.TimeServer.UI
{
    public class TimerTask04 : TimerObserver, ISyncTimerObserver
    {
        public TimerTask04(int timeValue, DateTimeTypeEnum dateTimeType) : base(timeValue, dateTimeType)
        {
        }

        public TimerTask04(DateTime dateTime, DateTimeTypeEnum dateTimeType) : base(dateTime, dateTimeType)
        {
        }

        public void Execute()
        {
            Console.WriteLine($"任务04{DateTime.Now}");
        }
    }
}
