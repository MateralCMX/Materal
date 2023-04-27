using Materal.Abstractions;
using Materal.TTA.ADONETRepository.Extensions;
using Materal.TTA.Common;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// 仓储帮助类
    /// </summary>
    public static class RepositoryHelper
    {
        /// <summary>
        /// DataReader转换为Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static TEntity? DataReaderConvertToEntity<TEntity>(IDataReader dataReader)
        where TEntity : class, new()
        {
            Type tType = typeof(TEntity);
            TEntity domain = new();
            PropertyInfo[] propertyInfos = tType.GetProperties();
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                string name = dataReader.GetName(i);
                PropertyInfo? propertyInfo = propertyInfos.FirstOrDefault(p => p.Name == name);
                if (propertyInfo == null) continue;
                if (dataReader.IsDBNull(i))
                {
                    propertyInfo.SetValue(domain, null);
                }
                else if (propertyInfo.PropertyType == typeof(string))
                {
                    propertyInfo.SetValue(domain, dataReader.GetString(i));
                }
                else if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?))
                {
                    propertyInfo.SetValue(domain, dataReader.GetInt32(i));
                }
                else if (propertyInfo.PropertyType == typeof(short) || propertyInfo.PropertyType == typeof(short?))
                {
                    propertyInfo.SetValue(domain, dataReader.GetInt16(i));
                }
                else if (propertyInfo.PropertyType == typeof(long) || propertyInfo.PropertyType == typeof(long?))
                {
                    propertyInfo.SetValue(domain, dataReader.GetInt64(i));
                }
                else if (propertyInfo.PropertyType == typeof(Guid) || propertyInfo.PropertyType == typeof(Guid?))
                {
                    propertyInfo.SetValue(domain, dataReader.GetGuid(i));
                }
                else if (propertyInfo.PropertyType == typeof(double) || propertyInfo.PropertyType == typeof(double?))
                {
                    propertyInfo.SetValue(domain, dataReader.GetDouble(i));
                }
                else if (propertyInfo.PropertyType == typeof(float) || propertyInfo.PropertyType == typeof(float?))
                {
                    propertyInfo.SetValue(domain, dataReader.GetFloat(i));
                }
                else if (propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(decimal?))
                {
                    propertyInfo.SetValue(domain, dataReader.GetDecimal(i));
                }
                else if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                {
                    propertyInfo.SetValue(domain, dataReader.GetDateTime(i));
                }
                else if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?))
                {
                    propertyInfo.SetValue(domain, dataReader.GetBoolean(i));
                }
                else if (propertyInfo.PropertyType == typeof(byte) || propertyInfo.PropertyType == typeof(byte?))
                {
                    propertyInfo.SetValue(domain, dataReader.GetByte(i));
                }
                else if (propertyInfo.PropertyType.IsAssignableTo(typeof(Enum)))
                {
                    Type valueType = Enum.GetUnderlyingType(propertyInfo.PropertyType);
                    if (valueType == typeof(byte))
                    {
                        propertyInfo.SetValue(domain, dataReader.GetByte(i));
                    }
                    else
                    {
                        propertyInfo.SetValue(domain, dataReader.GetInt32(i));
                    }
                }
            }
            return domain;
        }
    }
    /// <summary>
    /// 仓储帮助类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class RepositoryHelper<TEntity, TPrimaryKeyType> : IRepositoryHelper<TEntity, TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        private int _paramsIndex = 0;
        /// <summary>
        /// 设置查询命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="tableName"></param>
        /// <param name="unitOfWork"></param>
        public void SetQueryCommand(IDbCommand command, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrder sortOrder, string tableName, IADONETUnitOfWork unitOfWork)
        {
            StringBuilder tSql = GetQueryTSQL(command, expression, orderExpression, sortOrder, tableName, unitOfWork);
            command.CommandText = tSql.ToString();
        }
        /// <summary>
        /// 设置查询命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="tableName"></param>
        /// <param name="unitOfWork"></param>
        public void SetQueryCountCommand(IDbCommand command, Expression<Func<TEntity, bool>> expression, string tableName, IADONETUnitOfWork unitOfWork)
        {
            Type tType = typeof(TEntity);
            List<string> propertyNames = tType.GetProperties().Select(propertyInfo => propertyInfo.Name).ToList();
            string whereTSQLs = ExpressionToTSQL(command, expression, null, unitOfWork);
            StringBuilder tSql = new();
            tSql.AppendLine($"SELECT Count({nameof(IEntity<TPrimaryKeyType>.ID)})");
            tSql.AppendLine($"FROM {tableName}");
            if (!string.IsNullOrWhiteSpace(whereTSQLs))
            {
                tSql.AppendLine($"WHERE {whereTSQLs}");
            }
            command.CommandText = tSql.ToString();
        }
        /// <summary>
        /// 设置单行查询命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="tableName"></param>
        /// <param name="unitOfWork"></param>
        public void SetQueryOneRowCommand(IDbCommand command, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrder sortOrder, string tableName, IADONETUnitOfWork unitOfWork)
        {
            SetPagingCommand(command, expression, orderExpression, sortOrder, MateralConfig.PageStartNumber, 1, tableName, unitOfWork);
        }
        /// <summary>
        /// 设置分页查询命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="tableName"></param>
        /// <param name="unitOfWork"></param>
        public void SetPagingCommand(IDbCommand command, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrder sortOrder, int pageIndex, int pageSize, string tableName, IADONETUnitOfWork unitOfWork)
        {
            if (pageIndex < MateralConfig.PageStartNumber) pageIndex = MateralConfig.PageStartNumber;
            if (pageSize < 1) pageSize = 10;
            StringBuilder tSql = GetQueryTSQL(command, expression, orderExpression, sortOrder, tableName, unitOfWork);
            tSql.AppendLine(GetPagingTSQL(pageIndex, pageSize));
            command.CommandText = tSql.ToString();
        }
        /// <summary>
        /// 获得分页TSQL
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public abstract string GetPagingTSQL(int pageIndex, int pageSize);
        /// <summary>
        /// 排序表达式转换为排序TSQL
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public string OrderExpressionToTSQL(Expression<Func<TEntity, object>> expression, SortOrder sortOrder, IADONETUnitOfWork unitOfWork)
        {
            if (expression is not LambdaExpression orderExpression ||
                orderExpression.Body is not UnaryExpression unaryExpression ||
                unaryExpression.Operand is not MemberExpression memberExpression)
            {
                throw new TTAException("排序表达式错误");
            }
            string orderName = memberExpression.Member.Name;
            string sortOrderString = sortOrder switch
            {
                SortOrder.Ascending => "ASC",
                _ => "DESC"
            };
            string result = $"ORDER BY {unitOfWork.GetTSQLField(orderName)} {sortOrderString}";
            return result;
        }
        /// <summary>
        /// 表达式转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="parentExpression"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public string ExpressionToTSQL(IDbCommand command, Expression expression, Expression? parentExpression, IADONETUnitOfWork unitOfWork)
        {
            return expression switch
            {
                LambdaExpression lambdaExpression => ExpressionToTSQL(command, lambdaExpression.Body, lambdaExpression, unitOfWork),
                BinaryExpression binaryExpression => BinaryExpressionToTSQL(command, binaryExpression, unitOfWork),
                MemberExpression memberExpression => MemberExpressionToTSQL(command, memberExpression, unitOfWork),
                ConstantExpression constantExpression => ConstantExpressionToTSQL(command, constantExpression, parentExpression),
                MethodCallExpression methodCallExpression => MethodCallExpressionToTSQL(command, methodCallExpression, unitOfWork),
                UnaryExpression unaryExpression => UnaryExpressionToTSQL(command, unaryExpression, unitOfWork),
                _ => throw new TTAException("未识别的表达式")
            };
        }
        /// <summary>
        /// 一元表达式转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public string UnaryExpressionToTSQL(IDbCommand command, UnaryExpression expression, IADONETUnitOfWork unitOfWork)
        {
            if (expression.Operand is not MemberExpression memberExpresstion) throw new TTAException("未识别的表达式");
            string result = MemberExpressionToTSQL(command, memberExpresstion, unitOfWork);
            return result;
        }
        /// <summary>
        /// 方法调用表达式转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public string MethodCallExpressionToTSQL(IDbCommand command, MethodCallExpression expression, IADONETUnitOfWork unitOfWork)
        {
            if (expression.Method.Name == "Contains" && expression.Object is MemberExpression memberExpression)
            {
                if (memberExpression.Type == typeof(string))
                {
                    string leftTSQL = ExpressionToTSQL(command, memberExpression, expression, unitOfWork);
                    if (expression.Arguments.Count == 1 && expression.Arguments[0] is ConstantExpression constantExpression)
                    {
                        string vaue = $"%{constantExpression.Value}%";
                        string parameterName = $"@P{_paramsIndex++}";
                        command.AddParameter(parameterName, vaue);
                        return $"{leftTSQL} like {parameterName}";
                    }
                    else
                    {
                        throw new TTAException($"不支持方法{expression.Method.Name}转换为TSQL");
                    }
                }
                else if (memberExpression.Type.IsAssignableTo(typeof(System.Collections.ICollection)))
                {
                    if (expression.Arguments.Count == 1)
                    {
                        string leftTSQL = ExpressionToTSQL(command, expression.Arguments[0], memberExpression, unitOfWork);
                        string parameterName = $"@{memberExpression.Member.Name}";
                        if (memberExpression.Expression is ConstantExpression constantExpression)
                        {
                            object? value = null;
                            Type valueType = constantExpression.Value.GetType();
                            FieldInfo? fieldInfo = valueType.GetRuntimeField(memberExpression.Member.Name) ?? valueType.GetField(memberExpression.Member.Name);
                            if (fieldInfo != null)
                            {
                                value = fieldInfo.GetValue(constantExpression.Value);
                            }
                            else
                            {
                                PropertyInfo? propertyInfo = valueType.GetProperty(memberExpression.Member.Name);
                                if (propertyInfo != null)
                                {
                                    value = propertyInfo.GetValue(constantExpression.Value);
                                }
                            }
                            if (value == null || value is not IEnumerable values) throw new TTAException($"不支持方法{expression.Method.Name}转换为TSQL");
                            int index = 0;
                            List<string> parameterNames = new();
                            foreach (object? item in values)
                            {
                                if (item == null) continue;
                                string name = $"{parameterName}{index}";
                                command.AddParameter(name, item);
                                parameterNames.Add(name);
                            }
                            string result = $"{leftTSQL} in ({string.Join(",", parameterNames)})";
                            return result;
                        }
                    }
                    else
                    {
                        throw new TTAException($"不支持方法{expression.Method.Name}转换为TSQL");
                    }
                }
            }
            throw new TTAException($"不支持方法{expression.Method.Name}转换为TSQL");
        }
        /// <summary>
        /// 常量表达式转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="parentExpression"></param>
        /// <returns></returns>
        public string ConstantExpressionToTSQL(IDbCommand command, ConstantExpression? expression, Expression? parentExpression)
        {
            if (expression == null || expression.Value == null) return "IS NULL";
            if (parentExpression != null && parentExpression is MemberExpression parentMemberExpression)
            {
                FieldInfo? fieldInfo = expression.Value.GetType().GetField(parentMemberExpression.Member.Name);
                if (fieldInfo != null)
                {
                    object? value = fieldInfo.GetValue(expression.Value);
                    string parameterName = $"@{fieldInfo.Name}";
                    command.AddParameter(parameterName, value);
                    return parameterName;
                }
                return "IS NULL";
            }
            else
            {
                string parameterName = $"@P{_paramsIndex++}";
                command.AddParameter(parameterName, expression.Value);
                return parameterName;
            }
        }
        /// <summary>
        /// 成员表达式转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public string MemberExpressionToTSQL(IDbCommand command, MemberExpression expression, IADONETUnitOfWork unitOfWork)
        {
            switch (expression.Member.MemberType)
            {
                case MemberTypes.Field:
                    if (expression.Expression == null) return "IS NULL";
                    return ExpressionToTSQL(command, expression.Expression, expression, unitOfWork);
                case MemberTypes.Property:
                    if (expression.Member.DeclaringType != null &&
                        expression.Member.DeclaringType.FullName != null &&
                        expression.Member.DeclaringType.FullName.StartsWith("System.Nullable`1") &&
                        expression.Expression is MemberExpression trueMemberExpression)
                    {
                        if (expression.Type == typeof(bool) && expression.Member.Name == "HasValue")
                        {
                            return $"{unitOfWork.GetTSQLField(trueMemberExpression.Member.Name)} IS NOT NULL";
                        }
                        else if (expression.Member.Name == "Value")
                        {
                            return unitOfWork.GetTSQLField(trueMemberExpression.Member.Name);
                        }
                    }
                    return unitOfWork.GetTSQLField(expression.Member.Name);
                default:
                    throw new TTAException("未识别的表达式");
            }
        }
        /// <summary>
        /// 二元表达式转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public string BinaryExpressionToTSQL(IDbCommand command, BinaryExpression expression, IADONETUnitOfWork unitOfWork)
        {
            Expression left = expression.Left;
            Expression right = expression.Right;
            string leftTSQL = ExpressionToTSQL(command, left, expression, unitOfWork);
            string rightTSQL = ExpressionToTSQL(command, right, expression, unitOfWork);
            return expression.NodeType switch
            {
                ExpressionType.Equal => rightTSQL != "IS NULL" ? $"{leftTSQL} = {rightTSQL}" : $"{leftTSQL} {rightTSQL}",
                ExpressionType.NotEqual => $"{leftTSQL} != {rightTSQL}",
                ExpressionType.LessThan => $"{leftTSQL} < {rightTSQL}",
                ExpressionType.LessThanOrEqual => $"{leftTSQL} <= {rightTSQL}",
                ExpressionType.GreaterThan => $"{leftTSQL} > {rightTSQL}",
                ExpressionType.GreaterThanOrEqual => $"{leftTSQL} >= {rightTSQL}",
                ExpressionType.AndAlso => $"({leftTSQL} AND {rightTSQL})",
                ExpressionType.OrElse => $"({leftTSQL} OR {rightTSQL})",
                _ => throw new TTAException("未识别的表达式"),
            };
        }
        /// <summary>
        /// DataReader转换为Entity
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public TEntity? DataReaderConvertToDomain(IDataReader dataReader) => RepositoryHelper.DataReaderConvertToEntity<TEntity>(dataReader);
        /// <summary>
        /// 获得比较空TSQL
        /// </summary>
        /// <param name="notNullValue"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        public abstract string GetIsNullTSQL(string notNullValue, string nullValue);
        /// <summary>
        /// 获得查询语句
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="tableName"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        private StringBuilder GetQueryTSQL(IDbCommand command, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrder sortOrder, string tableName, IADONETUnitOfWork unitOfWork)
        {
            Type tType = typeof(TEntity);
            List<string> propertyNames = tType.GetProperties().Select(propertyInfo => unitOfWork.GetTSQLField(propertyInfo.Name)).ToList();
            StringBuilder tSql = new();
            tSql.AppendLine($"SELECT {string.Join(", ", propertyNames)}");
            tSql.AppendLine($"FROM {unitOfWork.GetTSQLField(tableName)}");
            string whereTSQLs = ExpressionToTSQL(command, expression, null, unitOfWork);
            if (!string.IsNullOrWhiteSpace(whereTSQLs))
            {
                tSql.AppendLine($"WHERE {whereTSQLs}");
            }
            string orderTSQL = OrderExpressionToTSQL(orderExpression, sortOrder, unitOfWork);
            tSql.AppendLine(orderTSQL);
            return tSql;
        }
    }
}
