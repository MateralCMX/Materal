using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;
using Microsoft.EntityFrameworkCore;

namespace RC.Core.SqlServer
{
    /// <summary>
    /// 发布中心仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class RCEFRepositoryImpl<T, TPrimaryKeyType> : SqliteEFRepositoryImpl<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected RCEFRepositoryImpl(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
