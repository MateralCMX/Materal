using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;

namespace Materal.TTA.SqlServerADONETRepository
{
    /// <summary>
    /// SqlServer仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public class SqlServerADONETRepositoryImpl<T, TPrimaryKeyType> : ADONETRepositoryImpl<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitOfWork"></param>
        public SqlServerADONETRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
