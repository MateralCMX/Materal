using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.TTA.SqlServerADONETRepository;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public static class SqlServerRepositoryHelper<T>
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
                if (propertyType.IsAssignableTo(typeof(string)))
                {
                    StringLengthAttribute? stringLengthAttribute = propertyInfo.GetCustomAttribute<StringLengthAttribute>();
                    if (stringLengthAttribute == null)
                    {
                        sqlType = SqlServerRepositoryHelper.GetTSQLField("varchar") + "(MAX)";
                    }
                    else
                    {
                        sqlType = SqlServerRepositoryHelper.GetTSQLField("varchar") + $"({stringLengthAttribute.MaximumLength})";
                    }
                }
                else if (propertyType.IsAssignableTo(typeof(Guid)))
                {
                    sqlType = SqlServerRepositoryHelper.GetTSQLField("uniqueidentifier");
                }
                else if (propertyType.IsAssignableTo(typeof(Enum)))
                {
                    Type enumValueType = Enum.GetUnderlyingType(propertyType);
                    if (enumValueType.IsAssignableTo(typeof(byte)))
                    {
                        sqlType = SqlServerRepositoryHelper.GetTSQLField("tinyint");
                    }
                    else
                    {
                        sqlType = SqlServerRepositoryHelper.GetTSQLField("int");
                    }
                }
                else if (propertyType.IsAssignableTo(typeof(int)))
                {
                    sqlType = SqlServerRepositoryHelper.GetTSQLField("int");
                }
                else throw new BusinessFlowException("未识别类型");
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
            tSqlBuilder.AppendLine($"\t{SqlServerRepositoryHelper.GetTSQLField(nameof(IDomain.ID))} {SqlServerRepositoryHelper.GetTSQLField("uniqueidentifier")} NOT NULL,");
            string fildeTSQL = GetCreateTableFildeTSQL();
            if (!string.IsNullOrWhiteSpace(fildeTSQL))
            {
                tSqlBuilder.Append(fildeTSQL);
            }
            tSqlBuilder.AppendLine($"\t{SqlServerRepositoryHelper.GetTSQLField(nameof(IDomain.CreateTime))} {SqlServerRepositoryHelper.GetTSQLField("datetime2")} NOT NULL,");
            tSqlBuilder.AppendLine($"\tCONSTRAINT {SqlServerRepositoryHelper.GetTSQLField($"PK_{tableName}")} PRIMARY KEY CLUSTERED({SqlServerRepositoryHelper.GetTSQLField(nameof(IDomain.ID))} ASC)");
            tSqlBuilder.AppendLine($"\tWITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON {SqlServerRepositoryHelper.GetTSQLField("PRIMARY")}");
            tSqlBuilder.AppendLine($") ON {SqlServerRepositoryHelper.GetTSQLField("PRIMARY")}");
            string result = tSqlBuilder.ToString();
            return result;
        }
    }
}
