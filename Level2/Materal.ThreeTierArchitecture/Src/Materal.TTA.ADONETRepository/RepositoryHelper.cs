using Materal.Abstractions;
using Materal.TTA.ADONETRepository.Extensions;
using Materal.TTA.Common;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

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
        public void SetQueryCommand(IDbCommand command, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, string tableName)
        {
            StringBuilder tSql = GetQueryTSQL(command, expression, orderExpression, sortOrder, tableName);
            command.CommandText = tSql.ToString();
        }
        /// <summary>
        /// 设置查询命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="tableName"></param>
        public void SetQueryCountCommand(IDbCommand command, Expression<Func<TEntity, bool>> expression, string tableName)
        {
            string whereTSQLs = ExpressionToTSQL(command, expression, null);
            StringBuilder tSql = new();
            tSql.AppendLine($"SELECT Count({GetTSQLField(nameof(IEntity<TPrimaryKeyType>.ID))})");
            tSql.AppendLine($"FROM {GetTSQLField(tableName)}");
            if (!string.IsNullOrWhiteSpace(whereTSQLs) && !whereTSQLs.StartsWith(GetParamsPrefix()))
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
        public void SetQueryOneRowCommand(IDbCommand command, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, string tableName)
        {
            SetPagingCommand(command, expression, orderExpression, sortOrder, MateralConfig.PageStartNumber, 1, tableName);
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
        public void SetPagingCommand(IDbCommand command, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, int pageIndex, int pageSize, string tableName)
        {
            if (pageIndex < MateralConfig.PageStartNumber) pageIndex = MateralConfig.PageStartNumber;
            if (pageSize < 1) pageSize = 10;
            StringBuilder tSql = GetQueryTSQL(command, expression, orderExpression, sortOrder, tableName);
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
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public string OrderExpressionToTSQL(Expression<Func<TEntity, object>> expression, SortOrderEnum sortOrder)
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
                SortOrderEnum.Ascending => "ASC",
                _ => "DESC"
            };
            string result = $"ORDER BY {GetTSQLField(orderName)} {sortOrderString}";
            return result;
        }
        /// <summary>
        /// 表达式转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="parentExpression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public string ExpressionToTSQL(IDbCommand command, Expression expression, Expression? parentExpression)
        {
            return expression switch
            {
                LambdaExpression lambdaExpression => ExpressionToTSQL(command, lambdaExpression.Body, lambdaExpression),
                BinaryExpression binaryExpression => BinaryExpressionToTSQL(command, binaryExpression),
                MemberExpression memberExpression => MemberExpressionToTSQL(command, memberExpression),
                ConstantExpression constantExpression => ConstantExpressionToTSQL(command, constantExpression, parentExpression),
                MethodCallExpression methodCallExpression => MethodCallExpressionToTSQL(command, methodCallExpression),
                UnaryExpression unaryExpression => UnaryExpressionToTSQL(command, unaryExpression),
                _ => throw new TTAException("未识别的表达式")
            };
        }
        /// <summary>
        /// 一元表达式转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public string UnaryExpressionToTSQL(IDbCommand command, UnaryExpression expression)
        {
            if (expression.Operand is not MemberExpression memberExpresstion) throw new TTAException("未识别的表达式");
            string result = MemberExpressionToTSQL(command, memberExpresstion);
            return result;
        }
        /// <summary>
        /// 包含方法转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        private string? ContainsMethodCallExpressionToTSQL(IDbCommand command, MethodCallExpression expression)
        {
            if (expression.Method.Name != "Contains") return null;
            if (expression.Object is MemberExpression objMemberExpression)
            {
                if (objMemberExpression.Type == typeof(string))
                {
                    string leftTSQL = ExpressionToTSQL(command, objMemberExpression, expression);
                    if (expression.Arguments.Count == 1 && expression.Arguments[0] is ConstantExpression constantExpression)
                    {
                        string vaue = $"%{constantExpression.Value}%";
                        string parameterName = GetParamsName($"P{_paramsIndex++}");
                        command.AddParameter(parameterName, vaue);
                        return $"{leftTSQL} like {parameterName}";
                    }
                    else
                    {
                        throw new TTAException($"不支持方法{expression.Method.Name}转换为TSQL");
                    }
                }
                else
                {
                    string leftTSQL = ExpressionToTSQL(command, expression.Arguments[0], objMemberExpression);
                    return ContainsMethodCallMemberExpressionToTSQL(command, leftTSQL, objMemberExpression);
                }
            }
            else if (expression.Method.ReturnType == typeof(bool) && expression.Arguments.Count == 2 && expression.Arguments[0] is MemberExpression argMemberExpression)
            {
                string leftTSQL = ExpressionToTSQL(command, expression.Arguments[1], argMemberExpression);
                return ContainsMethodCallMemberExpressionToTSQL(command, leftTSQL, argMemberExpression);
            }
            return null;
        }
        /// <summary>
        /// 包含方法转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="leftTSQL"></param>
        /// <param name="memberExpression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        private string? ContainsMethodCallMemberExpressionToTSQL(IDbCommand command, string leftTSQL, MemberExpression memberExpression)
        {
            if (!memberExpression.Type.IsAssignableTo(typeof(ICollection))) return null;
            string parameterName = GetParamsName(memberExpression.Member.Name);
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
                if (value == null || value is not IEnumerable values) throw new TTAException($"不支持方法Contains转换为TSQL");
                int index = 0;
                List<string> parameterNames = new();
                foreach (object? item in values)
                {
                    if (item == null) continue;
                    string name = $"{parameterName}{index++}";
                    command.AddParameter(name, item);
                    parameterNames.Add(name);
                }
                string result = $"{leftTSQL} in ({string.Join(",", parameterNames)})";
                return result;
            }
            return null;
        }
        /// <summary>
        /// 等于方法转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        private string? EqualsMethodCallExpressionToSQL(IDbCommand command, MethodCallExpression expression)
        {
            if (expression.Method.Name == "Equals" && expression.Object is MemberExpression memberExpression)
            {
                string leftTSQL = ExpressionToTSQL(command, memberExpression, expression);
                if (expression.Arguments.Count == 1)
                {
                    string parameterName = ExpressionToTSQL(command, expression.Arguments[0], expression);
                    return $"{leftTSQL}={parameterName}";
                }
                else
                {
                    throw new TTAException($"不支持方法{expression.Method.Name}转换为TSQL");
                }
            }
            return null;
        }
        /// <summary>
        /// 方法调用表达式转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public string MethodCallExpressionToTSQL(IDbCommand command, MethodCallExpression expression)
        {
            string? result = ContainsMethodCallExpressionToTSQL(command, expression);
            result ??= EqualsMethodCallExpressionToSQL(command, expression);
            return result ?? throw new TTAException($"不支持方法{expression.Method.Name}转换为TSQL");
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
                    string parameterName = GetParamsName(fieldInfo.Name);
                    command.AddParameter(parameterName, value);
                    return parameterName;
                }
                return "IS NULL";
            }
            else
            {
                string parameterName = GetParamsName($"P{_paramsIndex++}");
                command.AddParameter(parameterName, expression.Value);
                return parameterName;
            }
        }
        /// <summary>
        /// 成员表达式转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public string MemberExpressionToTSQL(IDbCommand command, MemberExpression expression)
        {
            if (expression.Member.MemberType == MemberTypes.Field && expression.Member is FieldInfo fieldInfo)
            {
                if (expression.Expression == null)
                {
                    string parameterName = GetParamsName(expression.Member.Name);
                    object targetObj = fieldInfo.ReflectedType.Instantiation(Array.Empty<object>());
                    object? trueValue = fieldInfo.GetValue(targetObj);
                    if (trueValue == null) return "IS NULL";
                    command.AddParameter(parameterName, trueValue);
                    return parameterName;
                }
                return ExpressionToTSQL(command, expression.Expression, expression);
            }
            if (expression.Member.MemberType == MemberTypes.Property && expression.Member is PropertyInfo propertyInfo)
            {
                if (expression.Expression == null)
                {
                    string parameterName = GetParamsName(expression.Member.Name);
                    object targetObj = propertyInfo.ReflectedType.Instantiation(Array.Empty<object>());
                    object? trueValue = propertyInfo.GetValue(targetObj);
                    if(trueValue == null)return "IS NULL";
                    command.AddParameter(parameterName, trueValue);
                    return parameterName;
                    
                }
                if (expression.Expression is ParameterExpression)
                {
                    return GetTSQLField(expression.Member.Name);
                }
                else if (expression.Expression is MemberExpression trueMemberExpression)
                {
                    if (expression.Member.DeclaringType != null &&
                        expression.Member.DeclaringType.FullName != null &&
                        expression.Member.DeclaringType.FullName.StartsWith("System.Nullable`1"))
                    {
                        if (expression.Type == typeof(bool) && expression.Member.Name == "HasValue")
                        {
                            return $"{GetTSQLField(trueMemberExpression.Member.Name)} IS NOT NULL";
                        }
                        else if (expression.Member.Name == "Value")
                        {
                            return GetTSQLField(trueMemberExpression.Member.Name);
                        }
                    }
                    else
                    {
                        string parameterName = GetParamsName(expression.Member.Name);
                        object? value = GetMemberValue(trueMemberExpression, expression.Member.Name);
                        if (value == null) return "IS NULL";
                        command.AddParameter(parameterName, value);
                        return parameterName;
                    }
                }
                else
                {
                    string result = ExpressionToTSQL(command, expression.Expression, expression);
                    return result;
                }
            }
            if (expression.Expression == null) return "IS NULL";
            throw new TTAException("未识别的表达式");
        }
        /// <summary>
        /// 获得最终值
        /// </summary>
        /// <param name="memberExpression"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private object? GetMemberValue(MemberExpression memberExpression, string name)
        {
            while (memberExpression.Expression is MemberExpression upMemberExpression)
            {
                memberExpression = upMemberExpression;
            }
            if(memberExpression.Expression is ConstantExpression constantExpression)
            {
                object? trueValue = constantExpression.Value ?? throw new TTAException("获取Member值失败");
                trueValue = memberExpression.Member.GetValue(trueValue);
                if (trueValue == null) throw new TTAException("获取Member值失败");
                PropertyInfo propertyInfo = trueValue.GetType().GetProperty(name) ?? throw new TTAException("获取Member值失败");
                trueValue = propertyInfo.GetValue(trueValue);
                return trueValue;
            }
            throw new TTAException("获取Member值失败");
        }
        /// <summary>
        /// 二元表达式转换为TSQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public string BinaryExpressionToTSQL(IDbCommand command, BinaryExpression expression)
        {
            Expression left = expression.Left;
            Expression right = expression.Right;
            string leftTSQL = ExpressionToTSQL(command, left, expression);
            string rightTSQL = ExpressionToTSQL(command, right, expression);
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
        /// <returns></returns>
        private StringBuilder GetQueryTSQL(IDbCommand command, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrderEnum sortOrder, string tableName)
        {
            Type tType = typeof(TEntity);
            List<string> propertyNames = tType.GetProperties().Select(propertyInfo => GetTSQLField(propertyInfo.Name)).ToList();
            StringBuilder tSql = new();
            tSql.AppendLine($"SELECT {string.Join(", ", propertyNames)}");
            tSql.AppendLine($"FROM {GetTSQLField(tableName)}");
            string whereTSQLs = ExpressionToTSQL(command, expression, null);
            if (!string.IsNullOrWhiteSpace(whereTSQLs) && !whereTSQLs.StartsWith(GetParamsPrefix()))
            {
                tSql.AppendLine($"WHERE {whereTSQLs}");
            }
            string orderTSQL = OrderExpressionToTSQL(orderExpression, sortOrder);
            tSql.AppendLine(orderTSQL);
            return tSql;
        }
        /// <summary>
        /// 获得参数名称
        /// </summary>
        /// <returns></returns>
        public abstract string GetTSQLField(string field);
        /// <summary>
        /// 获得参数名称
        /// </summary>
        /// <returns></returns>
        public abstract string GetParamsPrefix();
        /// <summary>
        /// 获得参数名称
        /// </summary>
        /// <returns></returns>
        public virtual string GetParamsName(string paramsName) => $"{GetParamsPrefix()}{paramsName}";
    }
}
