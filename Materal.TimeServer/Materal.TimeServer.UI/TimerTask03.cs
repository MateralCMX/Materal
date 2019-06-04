using Materal.DateTimeHelper;
using System;
using System.Threading.Tasks;

namespace Materal.TimeServer.UI
{
    public class TimerTask03 : TimerObserver, IAsyncTimerObserver
    {
        public TimerTask03(int timeValue, DateTimeTypeEnum dateTimeType) : base(timeValue, dateTimeType)
        {
        }

        public TimerTask03(DateTime dateTime, DateTimeTypeEnum dateTimeType) : base(dateTime, dateTimeType)
        {
        }

        public async Task ExecuteAsync()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("任务03");
            });
        }
    }
}
