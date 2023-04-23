using Materal.Abstractions;
using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.ADONETRepository;
using Materal.BusinessFlow.ADONETRepository.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class SqliteRepositoryHelper
    {
        /// <summary>
        /// 获得表是否存在的TSQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetTableExistsTSQL(BaseUnitOfWorkImpl unitOfWork, string tableName) => $"SELECT COUNT({unitOfWork.GetTSQLField("name")}) FROM {unitOfWork.GetTSQLField("sqlite_master")} WHERE {unitOfWork.GetTSQLField("type")}='table' AND {unitOfWork.GetTSQLField("name")}='{tableName}'";
    }
    public class SqliteRepositoryHelper<T> : BaseRepositoryHelper<T>
        where T : class, new()
    {
        /// <summary>
        /// 获得创建表字段TSQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        private static string GetCreateTableFildeTSQL(BaseUnitOfWorkImpl unitOfWork)
        {
            Type tType = typeof(T);
            PropertyInfo[] propertyInfos = tType.GetProperties();
            StringBuilder fildeTSQLBuilder = new();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.Name == nameof(IBaseDomain.ID) || propertyInfo.Name == nameof(IBaseDomain.CreateTime)) continue;
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
                else throw new BusinessFlowException("未识别类型");
                fildeTSQLBuilder.AppendLine($"\t{unitOfWork.GetTSQLField(propertyInfo.Name)} {sqlType} {isNull},");
            }
            return fildeTSQLBuilder.ToString();
        }
        /// <summary>
        /// 获得创建表TSQL
        /// </summary>
        /// <returns></returns>
        public static string GetCreateTableTSQL(BaseUnitOfWorkImpl unitOfWork, string tableName)
        {
            StringBuilder tSqlBuilder = new();
            tSqlBuilder.AppendLine($"CREATE TABLE {unitOfWork.GetTSQLField(tableName)}(");
            tSqlBuilder.AppendLine($"\t{unitOfWork.GetTSQLField(nameof(IBaseDomain.ID))} TEXT NOT NULL,");
            string fildeTSQL = GetCreateTableFildeTSQL(unitOfWork);
            if (!string.IsNullOrWhiteSpace(fildeTSQL))
            {
                tSqlBuilder.Append(fildeTSQL);
            }
            tSqlBuilder.AppendLine($"\t{unitOfWork.GetTSQLField(nameof(IBaseDomain.CreateTime))} DATETIME NOT NULL,");
            tSqlBuilder.AppendLine($"\tPRIMARY KEY ({unitOfWork.GetTSQLField(nameof(IBaseDomain.ID))})");
            tSqlBuilder.AppendLine($")");
            string result = tSqlBuilder.ToString();
            return result;
        }
        public override string GetPagingTSQL(int pageIndex, int pageSize)
        {
            return $"LIMIT {pageSize} OFFSET {(pageIndex - MateralConfig.PageStartNumber) * pageSize}";
        }
        public override string GetIsNullTSQL(string notNullValue, string nullValue)
        {
            return $"IFNULL({notNullValue}, {nullValue})";
        }
    }
}
