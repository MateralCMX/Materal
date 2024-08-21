using Materal.Oscillator.Abstractions.Oscillators;

namespace Materal.Oscillator.Abstractions.Extensions
{
    /// <summary>
    /// 调度器扩展
    /// </summary>
    internal static class OscillatorExtension
    {
        /// <summary>
        /// 获得分类
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        public static string GetGroup(this IOscillator oscillator) => oscillator.ID.ToString();
        /// <summary>
        /// 获得分类
        /// </summary>
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        public static string GetGroup(this IOscillatorData oscillatorData) => oscillatorData.ID.ToString();
    }
}
