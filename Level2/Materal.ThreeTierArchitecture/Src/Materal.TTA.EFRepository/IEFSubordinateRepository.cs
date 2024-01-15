namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// EF读写分离仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface IEFSubordinateRepository<TEntity, in TPrimaryKeyType> : ISubordinateRepository<TEntity, TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
