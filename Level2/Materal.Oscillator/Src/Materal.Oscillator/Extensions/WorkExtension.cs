namespace Materal.Oscillator.Extensions
{
    /// <summary>
    /// 作业扩展
    /// </summary>
    internal static class WorkExtension
    {
        /// <summary>
        /// 获得分类
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        public static string GetGroup(this IOscillator oscillator) => oscillator.ID.ToString();
        /// <summary>
        /// 获得JobKey
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        public static JobKey GetJobKey(this IOscillator oscillator)
        {
            string group = oscillator.GetGroup();
            JobKey jobKey = new(oscillator.WorkData.Name, group);
            return jobKey;
        }
        /// <summary>
        /// 转换为作业明细
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        public static IJobDetail CreateJobDetail(this IOscillator oscillator)
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
        public static IJobDetail CreateJobDetail(this IOscillator oscillator, JobKey jobKey)
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
