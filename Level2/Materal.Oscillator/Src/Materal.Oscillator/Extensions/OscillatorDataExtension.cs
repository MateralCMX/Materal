using Materal.Oscillator.Abstractions.Oscillators;

namespace Materal.Oscillator.Extensions
{
    /// <summary>
    /// 调度器数据扩展
    /// </summary>
    internal static class OscillatorDataExtension
    {
        /// <summary>
        /// 获得分类
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        public static string GetGroup(this IOscillatorData oscillator) => oscillator.ID.ToString();
        /// <summary>
        /// 获得JobKey
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        public static JobKey GetJobKey(this IOscillatorData oscillator) => oscillator.GetJobKey(oscillator.Work.Name);
        /// <summary>
        /// 获得JobKey
        /// </summary>
        /// <param name="oscillator"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static JobKey GetJobKey(this IOscillatorData oscillator, string name)
        {
            string group = oscillator.GetGroup();
            JobKey jobKey = new(name, group);
            return jobKey;
        }
        /// <summary>
        /// 转换为作业明细
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        public static IJobDetail CreateJobDetail(this IOscillatorData oscillator)
        {
            JobKey jobKey = oscillator.GetJobKey();
            return oscillator.CreateJobDetail(jobKey);
        }
        /// <summary>
        /// 转换为作业明细
        /// </summary>
        /// <param name="oscillator"></param>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        public static IJobDetail CreateJobDetail(this IOscillatorData oscillator, JobKey jobKey)
        {
            JobDataMap dataMap = new()
            {
                [ConstData.OscillatorKey] = oscillator
            };
            IJobDetail result = JobBuilder.Create<OscillatorJob>()
                .WithIdentity(jobKey)
                .UsingJobData(dataMap)
                .Build();
            return result;
        }
    }
}
