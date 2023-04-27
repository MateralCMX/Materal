using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.SqlServerEFRepository
{
    /// <summary>
    /// SqlServerEF仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class SqlServerEFRepositoryImpl<T, TPrimaryKeyType, TDBContext> : EFRepositoryImpl<T, TPrimaryKeyType, TDBContext>
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected SqlServerEFRepositoryImpl(TDBContext dbContext) : base(dbContext)
        {
        }
    }
}
