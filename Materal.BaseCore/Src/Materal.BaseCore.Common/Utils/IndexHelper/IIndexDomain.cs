namespace Materal.BaseCore.Common.Utils.IndexHelper
{
    public interface IIndexDomain
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
