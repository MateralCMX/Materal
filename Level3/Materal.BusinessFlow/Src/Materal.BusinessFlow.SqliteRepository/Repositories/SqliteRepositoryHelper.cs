using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.TTA.SqliteADONETRepository;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public static class SqliteRepositoryHelper<T>
        where T : class, new()
    {
        /// <summary>
        /// 获得创建表字段TSQL
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        private static string GetCreateTableFildeTSQL()
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
                fildeTSQLBuilder.AppendLine($"\t{SqliteRepositoryHelper.GetTSQLField(propertyInfo.Name)} {sqlType} {isNull},");
            }
            return fildeTSQLBuilder.ToString();
        }
        /// <summary>
        /// 获得创建表TSQL
        /// </summary>
        /// <returns></returns>
        public static string GetCreateTableTSQL(string tableName)
        {
            StringBuilder tSqlBuilder = new();
            tSqlBuilder.AppendLine($"CREATE TABLE {SqliteRepositoryHelper.GetTSQLField(tableName)}(");
            tSqlBuilder.AppendLine($"\t{SqliteRepositoryHelper.GetTSQLField(nameof(IBaseDomain.ID))} TEXT NOT NULL,");
            string fildeTSQL = GetCreateTableFildeTSQL();
            if (!string.IsNullOrWhiteSpace(fildeTSQL))
            {
                tSqlBuilder.Append(fildeTSQL);
            }
            tSqlBuilder.AppendLine($"\t{SqliteRepositoryHelper.GetTSQLField(nameof(IBaseDomain.CreateTime))} DATETIME NOT NULL,");
            tSqlBuilder.AppendLine($"\tPRIMARY KEY ({SqliteRepositoryHelper.GetTSQLField(nameof(IBaseDomain.ID))})");
            tSqlBuilder.AppendLine($")");
            string result = tSqlBuilder.ToString();
            return result;
        }
    }
}
