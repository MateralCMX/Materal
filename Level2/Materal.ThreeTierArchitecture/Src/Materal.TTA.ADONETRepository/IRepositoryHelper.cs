using Materal.TTA.Common;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// 仓储帮助
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface IRepositoryHelper<TEntity, in TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 设置查询命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="tableName"></param>
        void SetQueryCommand(IDbCommand command, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrder sortOrder, string tableName);
        /// <summary>
        /// 设置查询总数命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="tableName"></param>
        void SetQueryCountCommand(IDbCommand command, Expression<Func<TEntity, bool>> expression, string tableName);
        /// <summary>
        /// 设置查询一行命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="tableName"></param>
        void SetQueryOneRowCommand(IDbCommand command, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrder sortOrder, string tableName);
        /// <summary>
        /// 设置分页命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="tableName"></param>
        void SetPagingCommand(IDbCommand command, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrder sortOrder, int pageIndex, int pageSize, string tableName);
        /// <summary>
        /// 获得分页SQL
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        string GetPagingTSQL(int pageIndex, int pageSize);
        /// <summary>
        /// 排序表达式转换为TSQL
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        string OrderExpressionToTSQL(Expression<Func<TEntity, object>> expression, SortOrder sortOrder);
        /// <summary>
        /// 表达式转换为T-SQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="parentExpression"></param>
        /// <returns></returns>
        string ExpressionToTSQL(IDbCommand command, Expression expression, Expression? parentExpression);
        /// <summary>
        /// 一元表达式转换为T-SQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        string UnaryExpressionToTSQL(IDbCommand command, UnaryExpression expression);
        /// <summary>
        /// 方法表达式转换为T-SQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        string MethodCallExpressionToTSQL(IDbCommand command, MethodCallExpression expression);
        /// <summary>
        /// 常量表达式转换为T-SQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="parentExpression"></param>
        /// <returns></returns>
        string ConstantExpressionToTSQL(IDbCommand command, ConstantExpression? expression, Expression? parentExpression);
        /// <summary>
        /// 成员表达式转换为T-SQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        string MemberExpressionToTSQL(IDbCommand command, MemberExpression expression);
        /// <summary>
        /// 二重表达式转换为T-SQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        string BinaryExpressionToTSQL(IDbCommand command, BinaryExpression expression);
        /// <summary>
        /// 数据读取器转换为Domain
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        TEntity? DataReaderConvertToDomain(IDataReader dataReader);
        /// <summary>
        /// 获得为空TSQL
        /// </summary>
        /// <param name="notNullValue"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        string GetIsNullTSQL(string notNullValue, string nullValue);
    }
}
