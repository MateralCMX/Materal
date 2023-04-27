using Materal.Abstractions;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Materal.TTA.SqliteADONETRepository
{
    /// <summary>
    /// Sqlite仓储帮助类
    /// </summary>
    public static class SqliteRepositoryHelper
    {
        /// <summary>
        /// 获得表是否存在的TSQL
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetTableExistsTSQL(IADONETUnitOfWork unitOfWork, string tableName) => $"SELECT COUNT({unitOfWork.GetTSQLField("name")}) FROM {unitOfWork.GetTSQLField("sqlite_master")} WHERE {unitOfWork.GetTSQLField("type")}='table' AND {unitOfWork.GetTSQLField("name")}='{tableName}'";
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
        /// 获得创建表字段TSQL
        /// </summary>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        private static string GetCreateTableFildeTSQL(IADONETUnitOfWork unitOfWork)
        {
            Type tType = typeof(T);
            PropertyInfo[] propertyInfos = tType.GetProperties();
            StringBuilder fildeTSQLBuilder = new();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.Name == nameof(IEntity<TPrimaryKeyType>.ID)) continue;
                string isNull;
                string sqlType;
                Type propertyType = propertyInfo.PropertyType;
                if (propertyType.FullName.StartsWith("System.Nullable`1"))
                {
                    isNull = "NULL";
                    propertyType = propertyType.GenericTypeArguments.First();
                }
                else
                {
                    isNull = propertyInfo.GetCustomAttribute<RequiredAttribute>() == null ? "NULL" : "NOT NULL";
                }
                if (propertyType.IsAssignableTo(typeof(string)) || propertyType.IsAssignableTo(typeof(Guid)))
                {
                    sqlType = "TEXT";
                }
                else if (propertyType.IsAssignableTo(typeof(int)) || propertyType.IsAssignableTo(typeof(Enum)))
                {
                    sqlType = "INTEGER";
                }
                else throw new TTAException("未识别类型");
                fildeTSQLBuilder.AppendLine($"\t{unitOfWork.GetTSQLField(propertyInfo.Name)} {sqlType} {isNull},");
            }
            return fildeTSQLBuilder.ToString();
        }
        /// <summary>
        /// 获得创建表TSQL
        /// </summary>
        /// <returns></returns>
        public static string GetCreateTableTSQL(IADONETUnitOfWork unitOfWork, string tableName)
        {
            StringBuilder tSqlBuilder = new();
            tSqlBuilder.AppendLine($"CREATE TABLE {unitOfWork.GetTSQLField(tableName)}(");
            tSqlBuilder.AppendLine($"\t{unitOfWork.GetTSQLField(nameof(IEntity<TPrimaryKeyType>.ID))} TEXT NOT NULL,");
            string fildeTSQL = GetCreateTableFildeTSQL(unitOfWork);
            if (!string.IsNullOrWhiteSpace(fildeTSQL))
            {
                tSqlBuilder.Append(fildeTSQL);
            }
            tSqlBuilder.AppendLine($"\tPRIMARY KEY ({unitOfWork.GetTSQLField(nameof(IEntity<TPrimaryKeyType>.ID))})");
            tSqlBuilder.AppendLine($")");
            string result = tSqlBuilder.ToString();
            return result;
        }
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
    }
}
