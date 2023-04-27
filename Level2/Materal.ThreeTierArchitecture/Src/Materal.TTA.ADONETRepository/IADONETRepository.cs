using Materal.TTA.Common;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// ADONET仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface IADONETRepository<T, in TPrimaryKeyType> : IRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
