namespace Materal.MergeBlock.Repository
{
    /// <summary>
    /// 核心工作单元
    /// </summary>
    public interface IMergeBlockUnitOfWork : IEFUnitOfWork<Guid>, IEFUnitOfWork, IUnitOfWork<Guid>, IUnitOfWork
    {
    }
}
