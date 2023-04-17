using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.ADONETRepository;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Materal.Abstractions;
using Materal.BusinessFlow.ADONETRepository.Repositories;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class SqlServerRepositoryHelper
    {
        /// <summary>
        /// 获得表是否存在的TSQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetTableExistsTSQL(BaseUnitOfWorkImpl unitOfWork, string tableName) => $"SELECT COUNT({unitOfWork.GetTSQLField("name")}) FROM {unitOfWork.GetTSQLField("SysObjects")} WHERE {unitOfWork.GetTSQLField("name")}='{tableName}'";
    }
    public class SqlServerRepositoryHelper<T> : BaseRepositoryHelper<T>
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
                if (propertyType.IsAssignableTo(typeof(string)))
                {
                    StringLengthAttribute? stringLengthAttribute = propertyInfo.GetCustomAttribute<StringLengthAttribute>();
                    if(stringLengthAttribute == null)
                    {
                        sqlType = unitOfWork.GetTSQLField("varchar") + "(MAX)";
                    }
                    else
                    {
                        sqlType = unitOfWork.GetTSQLField("varchar") + $"({stringLengthAttribute.MaximumLength})";
                    }
                }
                else if (propertyType.IsAssignableTo(typeof(Guid)))
                {
                    sqlType = unitOfWork.GetTSQLField("uniqueidentifier");
                }
                else if (propertyType.IsAssignableTo(typeof(Enum)))
                {
                    Type enumValueType = Enum.GetUnderlyingType(propertyType);
                    if (enumValueType.IsAssignableTo(typeof(byte)))
                    {
                        sqlType = unitOfWork.GetTSQLField("tinyint");
                    }
                    else
                    {
                        sqlType = unitOfWork.GetTSQLField("int");
                    }
                }
                else if (propertyType.IsAssignableTo(typeof(int)))
                {
                    sqlType = unitOfWork.GetTSQLField("int");
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
            tSqlBuilder.AppendLine($"\t{unitOfWork.GetTSQLField(nameof(IBaseDomain.ID))} {unitOfWork.GetTSQLField("uniqueidentifier")} NOT NULL,");
            string fildeTSQL = GetCreateTableFildeTSQL(unitOfWork);
            if (!string.IsNullOrWhiteSpace(fildeTSQL))
            {
                tSqlBuilder.Append(fildeTSQL);
            }
            tSqlBuilder.AppendLine($"\t{unitOfWork.GetTSQLField(nameof(IBaseDomain.CreateTime))} {unitOfWork.GetTSQLField("datetime2")} NOT NULL,");
            tSqlBuilder.AppendLine($"\tCONSTRAINT {unitOfWork.GetTSQLField($"PK_{tableName}")} PRIMARY KEY CLUSTERED({unitOfWork.GetTSQLField(nameof(IBaseDomain.ID))} ASC)");
            tSqlBuilder.AppendLine($"\tWITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON {unitOfWork.GetTSQLField("PRIMARY")}");
            tSqlBuilder.AppendLine($") ON {unitOfWork.GetTSQLField("PRIMARY")}");
            string result = tSqlBuilder.ToString();
            return result;
        }
        public override string GetPagingTSQL(int pageIndex, int pageSize) => $"OFFSET {(pageIndex - MateralConfig.PageStartNumber) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        public override string GetIsNullTSQL(string notNullValue, string nullValue)
        {
            return $"ISNULL({notNullValue}, {nullValue})";
        }
    }
}
