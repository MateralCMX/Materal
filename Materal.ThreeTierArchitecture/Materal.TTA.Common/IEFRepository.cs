namespace Materal.TTA.Common
{
    public interface IEFRepository<T, in TPrimaryKeyType> : IRepository<T, TPrimaryKeyType> where T : class, IEntity<TPrimaryKeyType>
    {
    }
}
