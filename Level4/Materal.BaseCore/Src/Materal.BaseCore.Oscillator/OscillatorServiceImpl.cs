using Materal.BaseCore.Common;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;
using Microsoft.Extensions.Logging;

namespace Materal.BaseCore.Oscillator
{
    public class OscillatorServiceImpl : IOscillatorService
    {
        private readonly IScheduleRepository _sheduleRepository;
        private readonly IOscillatorHost _host;
        private readonly List<IOscillatorSchedule> _schedules = new();
        private readonly ILogger<OscillatorServiceImpl> _logger;
        public OscillatorServiceImpl(IScheduleRepository sheduleRepository, IOscillatorHost host, List<IOscillatorSchedule> schedules, ILogger<OscillatorServiceImpl> logger)
        {
            _sheduleRepository = sheduleRepository;
            _host = host;
            _logger = logger;
            _schedules = schedules;
        }
        public async Task StartAsync()
        {
            await InitAsync();
            await _host.StartAsync();
        }
        public async Task InitAsync()
        {
            _logger.LogInformation("正在初始化调度器...");
            List<string> allScheduleNames = _schedules.Select(m => m.ScheduleName).ToList();
            List<Schedule> allSchedules = await _sheduleRepository.FindAsync(m => allScheduleNames.Contains(m.Name));
            foreach (IOscillatorSchedule schedule in _schedules)
            {
                if (allSchedules.Any(m => m.Name == schedule.ScheduleName)) continue;
                Guid scheduleID = await schedule.AddSchedule(_host);
                _logger.LogInformation($"已新增调度器{schedule}");
                if (OscillatorDataHelper.IsIniting(schedule.WorkName)) continue;
                OscillatorDataHelper.SetInitKey(schedule.WorkName);
                OscillatorDataHelper.SetInitingKey(schedule.WorkName);
                await _host.RunNowAsync(scheduleID);
            }
        }
        public async Task RunNowAsync(string name)
        {
            IOscillatorSchedule schedule = _schedules.FirstOrDefault(m => m.Name == name) ?? throw new MateralCoreException("调度器不存在");
            Schedule? scheduleDomain = await _sheduleRepository.FirstOrDefaultAsync(m => m.Name == schedule.ScheduleName);
            Guid scheduleID = scheduleDomain == null ? await schedule.AddSchedule(_host) : scheduleDomain.ID;
            await _host.RunNowAsync(scheduleID);
        }
        public async Task RunNowAsync<T>() where T : IOscillatorSchedule, new() => await RunNowAsync(new T().Name);
        public async Task InitAsync(string name, string workName)
        {
            if (OscillatorDataHelper.IsIniting(workName)) return;
            OscillatorDataHelper.SetInitKey(workName);
            OscillatorDataHelper.SetInitingKey(workName);
            await RunNowAsync(name);
        }
        public async Task InitAsync<T>() where T : IOscillatorSchedule, new()
        {
            T schedule = new();
            await InitAsync(schedule.Name, schedule.WorkName);
        }
        public async Task RunNowAsync<T>(object data) where T : IOscillatorSchedule, new()
        {
            OscillatorDataHelper.SetData<T>(data);
            await RunNowAsync<T>();
        }
        public async Task RunNowAsync<TSchedule, TData>(TData data) where TSchedule : IOscillatorSchedule, new()
        {
            if(data == null)
            {
                await RunNowAsync<TSchedule>();
            }
            else
            {
                await RunNowAsync<TSchedule, object>(data);
            }
        }
    }
}
