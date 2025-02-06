using Materal.Oscillator.Abstractions.Oscillators;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.Abstractions.Extensions
{
    /// <summary>
    /// 调度器数据扩展
    /// </summary>
    public static class OscillatorDataExtensions
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        /// <summary>
        /// 获取调度器
        /// </summary>
        /// <param name="oscillatorData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<IOscillator?> GetOscillatorAsync(this IOscillatorData oscillatorData, IServiceProvider serviceProvider)
        {
            await _semaphore.WaitAsync();
            try
            {
                Type dataType = oscillatorData.GetType();
                if (IOscillatorData.MapperTable[dataType] is Type oscillatorType)
                {
                    if (serviceProvider.GetService(oscillatorType) is not IOscillator oscillator) return null;
                    await oscillator.SetDataAsync(oscillatorData);
                    return oscillator;
                }
                else
                {
                    IEnumerable<IOscillator> oscillators = serviceProvider.GetServices<IOscillator>();
                    IOscillator? oscillator = oscillators.FirstOrDefault(m => m.DataType == dataType);
                    if (oscillator is null) return null;
                    await oscillator.SetDataAsync(oscillatorData);
                    IOscillatorData.MapperTable[dataType] = oscillator.GetType();
                    return oscillator;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
