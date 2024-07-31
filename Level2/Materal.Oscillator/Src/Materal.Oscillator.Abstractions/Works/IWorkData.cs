using System.Collections;

namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 任务数据
    /// </summary>
    public interface IWorkData : IData
    {
        /// <summary>
        /// 映射表
        /// </summary>
        static Hashtable MapperTable { get; } = [];
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
    }
}
