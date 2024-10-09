using Materal.Extensions;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.MergeBlock.Oscillator.Abstractions
{
    /// <summary>
    /// 调度器扩展
    /// </summary>
    public static class IOscillatorHostExtensions
    {
        /// <summary>
        /// 初始化任务
        /// </summary>
        /// <param name="oscillatorHost"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<IOscillatorHost> InitWorkAsync<T>(this IOscillatorHost oscillatorHost, IServiceProvider serviceProvider)
            where T : IWorkData
        {
            Type workDataType = typeof(T);
            return await oscillatorHost.InitWorkAsync(serviceProvider, workDataType);
        }
        /// <summary>
        /// 初始化任务
        /// </summary>
        /// <param name="oscillatorHost"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="workDataType"></param>
        /// <returns></returns>
        public static async Task<IOscillatorHost> InitWorkAsync(this IOscillatorHost oscillatorHost, IServiceProvider serviceProvider, Type workDataType)
        {
            IWorkData workData = workDataType.InstantiationOrDefault<IWorkData>(serviceProvider) ?? throw new OscillatorException("实例化WorkData失败");
            return await oscillatorHost.InitWorkAsync(workData);
        }
        /// <summary>
        /// 初始化任务
        /// </summary>
        /// <param name="oscillatorHost"></param>
        /// <param name="workData"></param>
        /// <returns></returns>
        public static async Task<IOscillatorHost> InitWorkAsync(this IOscillatorHost oscillatorHost, IWorkData workData)
        {
            MergeBlockWorkInitManager.AddInitKey(workData);
            IPlanTriggerData planTrigger;
            if (workData is IMergeBlockWorkData mergeBlockWorkData)
            {
                planTrigger = mergeBlockWorkData.GetInitPlanTrigger();
            }
            else
            {
                planTrigger = new NowPlanTriggerData();
            }
            IOscillatorData oscillator = new NormalOscillatorData()
            {
                Enable = true,
                Work = workData,
                PlanTriggers = [planTrigger]
            };
            await oscillatorHost.StartOscillatorAsync(oscillator);
            return oscillatorHost;
        }
        /// <summary>
        /// 立即初始化任务
        /// </summary>
        /// <param name="oscillatorHost"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<IOscillatorHost> InitNowWorkAsync<T>(this IOscillatorHost oscillatorHost, IServiceProvider serviceProvider)
            where T : IWorkData
        {
            Type workDataType = typeof(T);
            return await oscillatorHost.InitNowWorkAsync(serviceProvider, workDataType);
        }
        /// <summary>
        /// 立即初始化任务
        /// </summary>
        /// <param name="oscillatorHost"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="workDataType"></param>
        /// <returns></returns>
        public static async Task<IOscillatorHost> InitNowWorkAsync(this IOscillatorHost oscillatorHost, IServiceProvider serviceProvider, Type workDataType)
        {
            IWorkData workData = workDataType.InstantiationOrDefault<IWorkData>(serviceProvider) ?? throw new OscillatorException("实例化WorkData失败");
            return await oscillatorHost.InitNowWorkAsync(workData);
        }
        /// <summary>
        /// 立即初始化任务
        /// </summary>
        /// <param name="oscillatorHost"></param>
        /// <param name="workData"></param>
        /// <returns></returns>
        public static async Task<IOscillatorHost> InitNowWorkAsync(this IOscillatorHost oscillatorHost, IWorkData workData)
        {
            MergeBlockWorkInitManager.AddInitKey(workData);
            IOscillatorData oscillator = new NormalOscillatorData()
            {
                Enable = true,
                Work = workData,
                PlanTriggers = [new NowPlanTriggerData()]
            };
            await oscillatorHost.StartOscillatorAsync(oscillator);
            return oscillatorHost;
        }
        /// <summary>
        /// 立即执行任务
        /// </summary>
        /// <param name="oscillatorHost"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<IOscillatorHost> RunNowWorkAsync<T>(this IOscillatorHost oscillatorHost, IServiceProvider serviceProvider)
            where T : IWorkData
        {
            Type workDataType = typeof(T);
            return await oscillatorHost.RunNowWorkAsync(serviceProvider, workDataType);
        }
        /// <summary>
        /// 立即执行任务
        /// </summary>
        /// <param name="oscillatorHost"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="workDataType"></param>
        /// <returns></returns>
        public static async Task<IOscillatorHost> RunNowWorkAsync(this IOscillatorHost oscillatorHost, IServiceProvider serviceProvider, Type workDataType)
        {
            IWorkData workData = workDataType.InstantiationOrDefault<IWorkData>(serviceProvider) ?? throw new OscillatorException("实例化WorkData失败");
            return await oscillatorHost.RunNowWorkAsync(workData);
        }
        /// <summary>
        /// 立即执行任务
        /// </summary>
        /// <param name="oscillatorHost"></param>
        /// <param name="workData"></param>
        /// <returns></returns>
        public static async Task<IOscillatorHost> RunNowWorkAsync(this IOscillatorHost oscillatorHost, IWorkData workData)
        {
            IOscillatorData oscillator = new NormalOscillatorData()
            {
                Enable = true,
                Work = workData,
                PlanTriggers = [new NowPlanTriggerData()]
            };
            await oscillatorHost.StartOscillatorAsync(oscillator);
            return oscillatorHost;
        }
    }
}
