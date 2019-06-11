using Quartz;
using System.Threading.Tasks;

namespace Materal.Dispatcher.QuartzNet
{
    public interface IScheduleManager
    {
        Task<IScheduler> BuildScheduler(SchedulerConfigModel config);
    }
}
