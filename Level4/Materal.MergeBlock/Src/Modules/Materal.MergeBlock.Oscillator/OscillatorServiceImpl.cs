namespace Materal.BaseCore.Oscillator
{
    /// <summary>
    /// 调度器服务
    /// </summary>
    public class OscillatorServiceImpl(IScheduleRepository sheduleRepository, IOscillatorHost host, List<IOscillatorSchedule> schedules, ILogger<OscillatorServiceImpl> logger) : IOscillatorService
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            await InitAsync();
            await host.StartAsync();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
        {
            logger.LogInformation("正在初始化调度器...");
            List<string> allScheduleNames = schedules.Select(m => m.ScheduleName).ToList();
            List<Schedule> allSchedules = await sheduleRepository.FindAsync(m => allScheduleNames.Contains(m.Name));
            foreach (IOscillatorSchedule schedule in schedules)
            {
                if (allSchedules.Any(m => m.Name == schedule.ScheduleName)) continue;
                Guid scheduleID = await schedule.AddSchedule(host);
                logger.LogInformation($"已新增调度器{schedule}");
                if (OscillatorDataHelper.IsIniting(schedule.WorkName)) continue;
                OscillatorDataHelper.SetInitKey(schedule.WorkName);
                OscillatorDataHelper.SetInitingKey(schedule.WorkName);
                await host.RunNowAsync(scheduleID);
            }
        }
        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        public async Task RunNowAsync(string name)
        {
            IOscillatorSchedule schedule = schedules.FirstOrDefault(m => m.Name == name) ?? throw new MergeBlockException("调度器不存在");
            Schedule? scheduleDomain = await sheduleRepository.FirstOrDefaultAsync(m => m.Name == schedule.ScheduleName);
            Guid scheduleID = scheduleDomain == null ? await schedule.AddSchedule(host) : scheduleDomain.ID;
            await host.RunNowAsync(scheduleID);
        }
        /// <summary>
        /// 立即执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task RunNowAsync<T>() where T : IOscillatorSchedule, new() => await RunNowAsync(new T().Name);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="workName"></param>
        /// <returns></returns>
        public async Task InitAsync(string name, string workName)
        {
            if (OscillatorDataHelper.IsIniting(workName)) return;
            OscillatorDataHelper.SetInitKey(workName);
            OscillatorDataHelper.SetInitingKey(workName);
            await RunNowAsync(name);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task InitAsync<T>() where T : IOscillatorSchedule, new()
        {
            T schedule = new();
            await InitAsync(schedule.Name, schedule.WorkName);
        }
        /// <summary>
        /// 立即执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task RunNowAsync<T>(object data) where T : IOscillatorSchedule, new()
        {
            OscillatorDataHelper.SetData<T>(data);
            await RunNowAsync<T>();
        }
        /// <summary>
        /// 立即执行
        /// </summary>
        /// <typeparam name="TSchedule"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
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
