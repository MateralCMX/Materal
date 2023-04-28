using Materal.Abstractions;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.Common.Model;

namespace Materal.TTA.SqliteADONETRepository
{
    /// <summary>
    /// Sqlite仓储帮助类
    /// </summary>
    public static class SqliteRepositoryHelper
    {
        /// <summary>
        /// 获得参数前缀
        /// </summary>
        /// <returns></returns>
        public static string GetParamsPrefix() => SqliteConfigModel.ParamsPrefix;
        /// <summary>
        /// 获得TSQL字段
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetTSQLField(string field) => $"{SqliteConfigModel.FieldPrefix}{field}{SqliteConfigModel.FieldSuffix}";
        /// <summary>
        /// 获得表是否存在的TSQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetTableExistsTSQL(string tableName) => $"SELECT COUNT({GetTSQLField("name")}) FROM {GetTSQLField("sqlite_master")} WHERE {GetTSQLField("type")}='table' AND {GetTSQLField("name")}='{tableName}'";
    }
    /// <summary>
    /// Sqlite仓储帮助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public class SqliteRepositoryHelper<T, TPrimaryKeyType> : RepositoryHelper<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 获得分页TSQL
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public override string GetPagingTSQL(int pageIndex, int pageSize)
        {
            return $"LIMIT {pageSize} OFFSET {(pageIndex - MateralConfig.PageStartNumber) * pageSize}";
        }
        /// <summary>
        /// 获得判断空TSQL
        /// </summary>
        /// <param name="notNullValue"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public override string GetIsNullTSQL(string notNullValue, string nullValue)
        {
            return $"IFNULL({notNullValue}, {nullValue})";
        }
        /// <summary>
        /// 获得参数前缀
        /// </summary>
        /// <returns></returns>
        public override string GetParamsPrefix() => SqliteConfigModel.ParamsPrefix;
        /// <summary>
        /// 获得TSQL字段
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public override string GetTSQLField(string field) => SqliteRepositoryHelper.GetTSQLField(field);
    }
}
