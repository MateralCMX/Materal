namespace Materal.TTA.Common
{
    public interface IEntityFrameworkRepository<T, in TPrimaryKeyType> : IRepository<T, TPrimaryKeyType>
    {
    }
}
