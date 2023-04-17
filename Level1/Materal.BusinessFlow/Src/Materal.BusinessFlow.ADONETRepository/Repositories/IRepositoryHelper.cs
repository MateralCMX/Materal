using Materal.BusinessFlow.Abstractions;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Materal.BusinessFlow.ADONETRepository.Repositories
{
    public interface IRepositoryHelper<T>
        where T : class, new()
    {
        /// <summary>
        /// 设置查询命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="tableName"></param>
        /// <param name="unitOfWork"></param>
        public void SetQueryCommand(IDbCommand command, Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, string tableName, BaseUnitOfWorkImpl unitOfWork);
        /// <summary>
        /// 设置查询总数命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="tableName"></param>
        /// <param name="unitOfWork"></param>
        public void SetQueryCountCommand(IDbCommand command, Expression<Func<T, bool>> expression, string tableName, BaseUnitOfWorkImpl unitOfWork);
        /// <summary>
        /// 设置查询一行命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="tableName"></param>
        /// <param name="unitOfWork"></param>
        public void SetQueryOneRowCommand(IDbCommand command, Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, string tableName, BaseUnitOfWorkImpl unitOfWork);
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
        /// <param name="unitOfWork"></param>
        public void SetPagingCommand(IDbCommand command, Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pageIndex, int pageSize, string tableName, BaseUnitOfWorkImpl unitOfWork);
        /// <summary>
        /// 获得分页SQL
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public string GetPagingTSQL(int pageIndex, int pageSize);
        /// <summary>
        /// 排序表达式转换为TSQL
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public string OrderExpressionToTSQL(Expression<Func<T, object>> expression, SortOrder sortOrder, BaseUnitOfWorkImpl unitOfWork);
        /// <summary>
        /// 表达式转换为T-SQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <param name="parentExpression"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        public string ExpressionToTSQL(IDbCommand command, Expression expression, Expression? parentExpression, BaseUnitOfWorkImpl unitOfWork);
        /// <summary>
        /// 一元表达式转换为T-SQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        public string UnaryExpressionToTQL(IDbCommand command, UnaryExpression expression, BaseUnitOfWorkImpl unitOfWork);
        /// <summary>
        /// 方法表达式转换为T-SQL
        /// </summary>
        /// <param name="command"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string MethodCallExpressionToTSQL(IDbCommand command, MethodCallExpression expression, BaseUnitOfWorkImpl unitOfWork);
        /// <summary>
        /// 常量表达式转换为T-SQL
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string ConstantExpressionToTSQL(IDbCommand command, ConstantExpression? expression, Expression? parentExpression);
        /// <summary>
        /// 成员表达式转换为T-SQL
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        public string MemberExpressionToTSQL(IDbCommand command, MemberExpression expression, BaseUnitOfWorkImpl unitOfWork);
        /// <summary>
        /// 二重表达式转换为T-SQL
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        public string BinaryExpressionToTSQL(IDbCommand command, BinaryExpression expression, BaseUnitOfWorkImpl unitOfWork);
        /// <summary>
        /// 数据读取器转换为Domain
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public T? DataReaderConvertToDomain(IDataReader dataReader);
        string GetIsNullTSQL(string notNullValue, string nullValue);
    }
}
