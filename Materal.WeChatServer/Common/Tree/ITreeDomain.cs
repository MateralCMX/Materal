namespace Common.Tree
{
    public interface ITreeDomain<T> where T : struct
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        T ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        T? ParentID { get; set; }
    }
}
