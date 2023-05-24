using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.Abstractions.Works;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleDemo.Works
{
    /// <summary>
    /// 
    /// </summary>
    public class DemoWork : BaseWork<DemoWorkData>, IWork<DemoWorkData>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkRepository _workRepository;
        private readonly ILogger<DemoWork>? _logger;
        public DemoWork(IServiceProvider serviceProvider, IWorkRepository workRepository)
        {
            _serviceProvider = serviceProvider;
            _workRepository = workRepository;
            _logger = _serviceProvider.GetService<ILogger<DemoWork>>();
        }
        public override async Task<string?> ExcuteAsync(DemoWorkData workData, List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work)
        {
            _logger?.LogInformation("--------------------------------------------------------");
            _logger?.LogInformation(workData.Message);
            _logger?.LogInformation("--------------------------------------------------------");
            List<Work> works = await _workRepository.FindAsync(m => true);
            foreach (Work item in works)
            {
                _logger?.LogInformation(item.Name);
            }
            return null;
        }
    }
}
