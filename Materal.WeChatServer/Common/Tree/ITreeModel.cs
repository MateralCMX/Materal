using System.Collections.Generic;

namespace Common.Tree
{
    public interface ITreeModel<T1, T2> where T1 : ITreeModel<T1, T2> where T2 : struct
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        T2 ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        ICollection<T1> Child { get; set; }
    }
}
