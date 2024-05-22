using Materal.MergeBlock.Abstractions.Oscillator;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.MergeBlock.Oscillator
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
        /// <exception cref="OscillatorException"></exception>
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
        /// <exception cref="OscillatorException"></exception>
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
        /// <exception cref="OscillatorException"></exception>
        public static async Task<IOscillatorHost> InitWorkAsync(this IOscillatorHost oscillatorHost, IWorkData workData)
        {
            OscillatorInitManager.AddInitKey(workData);
            await oscillatorHost.RunNowWorkDataAsync(workData);
            return oscillatorHost;
        }
    }
}
