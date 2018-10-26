using Materal.TTA.Common;

namespace Materal.TTA.MySqlRepository
{
    /// <summary>
    /// MySql仓储
    /// </summary>
    public interface IMySqlRepository<T,in TPrimaryKeyType> : IEntityFrameworkRepository<T, TPrimaryKeyType>
    {
    }
}
