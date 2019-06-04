using Materal.DateTimeHelper;
using System;
using System.Threading.Tasks;

namespace Materal.TimeServer.UI
{
    public class TimerTask01 : TimerObserver, IAsyncTimerObserver
    {
        public TimerTask01(int timeValue, DateTimeTypeEnum dateTimeType) : base(timeValue, dateTimeType)
        {
        }

        public TimerTask01(DateTime dateTime, DateTimeTypeEnum dateTimeType) : base(dateTime, dateTimeType)
        {
        }

        public async Task ExecuteAsync()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("任务01");
            });
        }
    }
}
