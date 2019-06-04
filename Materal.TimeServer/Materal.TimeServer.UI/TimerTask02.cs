using Materal.DateTimeHelper;
using System;

namespace Materal.TimeServer.UI
{
    public class TimerTask02 : TimerObserver, ISyncTimerObserver
    {
        public TimerTask02(int timeValue, DateTimeTypeEnum dateTimeType) : base(timeValue, dateTimeType)
        {
        }

        public TimerTask02(DateTime dateTime, DateTimeTypeEnum dateTimeType) : base(dateTime, dateTimeType)
        {
        }

        public void Execute()
        {
            Console.WriteLine("任务02");
        }
    }
}
