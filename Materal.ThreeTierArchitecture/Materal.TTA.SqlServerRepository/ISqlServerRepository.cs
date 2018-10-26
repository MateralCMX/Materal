using Materal.TTA.Common;

namespace Materal.TTA.SqlServerRepository
{
    /// <summary>
    /// SqlServer仓储
    /// </summary>
    public interface ISqlServerRepository<T,in TPrimaryKeyType> : IEntityFrameworkRepository<T, TPrimaryKeyType>
    {
    }
}
