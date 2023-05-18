using Materal.Abstractions;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.Common.Model;

namespace Materal.TTA.SqlServerADONETRepository
{
    /// <summary>
    /// SqlServer仓储帮助类
    /// </summary>
    public class SqlServerRepositoryHelper
    {
        /// <summary>
        /// 获得参数
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static string GetParams(string paramName) => $"{SqlServerConfigModel.ParamsPrefix}{paramName}";
        /// <summary>
        /// 获得TSQL字段
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetTSQLField(string field) => $"{SqlServerConfigModel.FieldPrefix}{field}{SqlServerConfigModel.FieldSuffix}";
        /// <summary>
        /// 获得表是否存在的TSQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetTableExistsTSQL(string tableName) => $"SELECT COUNT({GetTSQLField("name")}) FROM {GetTSQLField("SysObjects")} WHERE {GetTSQLField("name")}='{tableName}'";
    }
    /// <summary>
    /// SqlServer仓储帮助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public class SqlServerRepositoryHelper<T, TPrimaryKeyType> : RepositoryHelper<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public SqlServerRepositoryHelper(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        /// <summary>
        /// 获得分页TSQL
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public override string GetPagingTSQL(int pageIndex, int pageSize) => $"OFFSET {(pageIndex - MateralConfig.PageStartNumber) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        /// <summary>
        /// 获得判断空TSQL
        /// </summary>
        /// <param name="notNullValue"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public override string GetIsNullTSQL(string notNullValue, string nullValue)
        {
            return $"ISNULL({notNullValue}, {nullValue})";
        }
        /// <summary>
        /// 获得参数前缀
        /// </summary>
        /// <returns></returns>
        public override string GetParamsPrefix() => SqlServerConfigModel.ParamsPrefix;
        /// <summary>
        /// 获得TSQL字段
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public override string GetTSQLField(string field) => SqlServerRepositoryHelper.GetTSQLField(field);
    }
}
