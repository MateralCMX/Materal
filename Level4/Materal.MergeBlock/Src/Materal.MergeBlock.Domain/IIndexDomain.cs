namespace Materal.MergeBlock.Domain
{
    public interface IIndexDomain : IDomain
    {
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; set; }
    }
}
