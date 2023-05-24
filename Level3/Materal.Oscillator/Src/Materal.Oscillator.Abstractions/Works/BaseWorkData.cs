using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 任务数据基类
    /// </summary>
    public class BaseWorkData : IWorkData
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="workData"></param>
        /// <returns></returns>
        public virtual IWorkData Deserialization(string workData) => (IWorkData)workData.JsonToObject(GetType());
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public virtual IWorkData Deserialization(Work work) => Deserialization(work.WorkData);
    }
}
