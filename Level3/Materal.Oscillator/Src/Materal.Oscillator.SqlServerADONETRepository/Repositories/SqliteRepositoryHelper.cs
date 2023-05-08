using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.TTA.SqlServerADONETRepository;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Materal.Oscillator.SqlServerRepository.Repositories
{
    /// <summary>
    /// SqlServer仓储帮助
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SqlServerRepositoryHelper<T>
        where T : class, new()
    {
        /// <summary>
        /// 获得创建表字段TSQL
        /// </summary>
        /// <returns></returns>
        /// <exception cref="OscillatorException"></exception>
        private static string GetCreateTableFildeTSQL()
        {
            Type tType = typeof(T);
            PropertyInfo[] propertyInfos = tType.GetProperties();
            StringBuilder fildeTSQLBuilder = new();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.Name == nameof(IDomain.ID) || propertyInfo.Name == nameof(IDomain.CreateTime)) continue;
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
                else throw new OscillatorException("未识别类型");
                fildeTSQLBuilder.AppendLine($"\t{SqlServerRepositoryHelper.GetTSQLField(propertyInfo.Name)} {sqlType} {isNull},");
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
            tSqlBuilder.AppendLine($"CREATE TABLE {SqlServerRepositoryHelper.GetTSQLField(tableName)}(");
            tSqlBuilder.AppendLine($"\t{SqlServerRepositoryHelper.GetTSQLField(nameof(IDomain.ID))} TEXT NOT NULL,");
            string fildeTSQL = GetCreateTableFildeTSQL();
            if (!string.IsNullOrWhiteSpace(fildeTSQL))
            {
                tSqlBuilder.Append(fildeTSQL);
            }
            tSqlBuilder.AppendLine($"\t{SqlServerRepositoryHelper.GetTSQLField(nameof(IDomain.CreateTime))} DATETIME NOT NULL,");
            tSqlBuilder.AppendLine($"\tPRIMARY KEY ({SqlServerRepositoryHelper.GetTSQLField(nameof(IDomain.ID))})");
            tSqlBuilder.AppendLine($")");
            string result = tSqlBuilder.ToString();
            return result;
        }
    }
}
