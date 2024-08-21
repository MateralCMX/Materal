namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// EF仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface IEFRepository<TEntity, in TPrimaryKeyType> : IRepository<TEntity, TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
