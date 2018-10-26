using Materal.TTA.Common;

namespace Materal.TTA.SqliteRepository
{
    /// <summary>
    /// Sqlite仓储
    /// </summary>
    public interface ISqliteRepository<T,in TPrimaryKeyType> : IEntityFrameworkRepository<T, TPrimaryKeyType>
    {
    }
}
