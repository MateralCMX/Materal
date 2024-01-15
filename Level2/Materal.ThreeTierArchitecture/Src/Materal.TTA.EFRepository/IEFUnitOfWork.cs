namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// EF工作单元
    /// </summary>
    public interface IEFUnitOfWork : IUnitOfWork
    {

    }
    /// <summary>
    /// EF工作单元
    /// </summary>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface IEFUnitOfWork<TPrimaryKeyType> : IEFUnitOfWork, IUnitOfWork<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
