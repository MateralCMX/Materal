using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    /// <summary>
    /// Sqlite仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public class SqliteADONETRepositoryImpl<T, TPrimaryKeyType> : ADONETRepositoryImpl<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitOfWork"></param>
        public SqliteADONETRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
