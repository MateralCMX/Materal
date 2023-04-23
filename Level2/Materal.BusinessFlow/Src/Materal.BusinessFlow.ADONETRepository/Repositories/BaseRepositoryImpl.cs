using Materal.Abstractions;
using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.BusinessFlow.ADONETRepository.Extensions;
using Materal.Utils.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Materal.BusinessFlow.ADONETRepository.Repositories
{
    public abstract class BaseRepositoryImpl<T> : IBaseRepository<T>
        where T : class, IBaseDomain, new()
    {
        protected virtual string TableName => typeof(T).Name;
        protected readonly BaseUnitOfWorkImpl UnitOfWork;
        protected readonly IRepositoryHelper<T> RepositoryHelper;
        protected readonly ILogger? Logger;
        private static bool _isFirst = true;
        private static readonly object _initLockObject = new();
        public BaseRepositoryImpl(IUnitOfWork unitOfWork)
        {
            if (unitOfWork is not BaseUnitOfWorkImpl unitOfWorkImpl) throw new BusinessFlowException("工作单元类型错误");
            UnitOfWork = unitOfWorkImpl;
            RepositoryHelper = unitOfWorkImpl.ServiceProvider.GetService<IRepositoryHelper<T>>() ?? throw new BusinessFlowException("未找到仓储帮助类");
            ILoggerFactory? loggerFactory = unitOfWorkImpl.ServiceProvider.GetService<ILoggerFactory>();
            Logger = loggerFactory?.CreateLogger(GetType());
            InitDB();
        }
        public virtual void InitDB()
        {
            if (!_isFirst) return;
            lock (_initLockObject)
            {
                if (!_isFirst) return;
                UnitOfWork.OperationDB(connection =>
                {
                    IDbCommand command = connection.CreateCommand();
                    command.CommandText = GetTableExistsTSQL(TableName);
                    Logger?.LogDebugTSQL(command);
                    using (IDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr.GetInt32(0) > 0) return;
                        }
                    }
                    IDbCommand createTableCommand = connection.CreateCommand();
                    createTableCommand.CommandText = GetCreateTableTSQL();
                    Logger?.LogDebugTSQL(createTableCommand);
                    createTableCommand.ExecuteNonQuery();
                });
                _isFirst = false;
            }
        }
        public bool Existing(Guid id) => FirstOrDefault(id) != null;
        public T First(Guid id) => FirstOrDefault(id) ?? throw new BusinessFlowException("数据不存在");
        public T First(Expression<Func<T, bool>> filterExpression) => FirstOrDefault(filterExpression) ?? throw new BusinessFlowException("数据不存在");
        public T? FirstOrDefault(Guid id) => FirstOrDefault(m => m.ID == id);
        public T? FirstOrDefault(Expression<Func<T, bool>> filterExpression)
        {
            T? result = null;
            UnitOfWork.OperationDB(connection =>
            {
                IDbCommand command = connection.CreateCommand();
                RepositoryHelper.SetQueryOneRowCommand(command, filterExpression, m => m.CreateTime, SortOrder.Descending, TableName, UnitOfWork);
                Logger?.LogDebugTSQL(command);
                Type tType = typeof(T);
                using IDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    result = RepositoryHelper.DataReaderConvertToDomain(dr);
                }
            });
            return result;
        }
        public List<T> GetList(IQueryModel? queryModel)
            => GetList(queryModel == null ? m => true : queryModel.GetQueryExpression<T>());
        public List<T> GetList(Expression<Func<T, bool>> filterExpression)
        {
            List<T> result = new();
            UnitOfWork.OperationDB(connection =>
            {
                IDbCommand command = connection.CreateCommand();
                RepositoryHelper.SetQueryCommand(command, filterExpression, m => m.CreateTime, SortOrder.Descending, TableName, UnitOfWork);
                Logger?.LogDebugTSQL(command);
                Type tType = typeof(T);
                using IDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    T? domain = RepositoryHelper.DataReaderConvertToDomain(dr);
                    if (domain == null) continue;
                    result.Add(domain);
                }
            });
            return result;
        }
        public (List<T> data, PageModel pageInfo) Paging(IQueryModel? queryModel = null) => Paging(queryModel, m => m.CreateTime, SortOrder.Descending);
        public (List<T> data, PageModel pageInfo) Paging(IQueryModel? queryModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (queryModel == null)
            {
                return Paging(m => true, orderExpression, sortOrder, MateralConfig.PageStartNumber, 10);
            }
            else
            {
                return Paging(queryModel.GetQueryExpression<T>(), orderExpression, sortOrder, queryModel.PageIndex, queryModel.PageSize);
            }
        }
        public (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filterExpression, int pageIndex, int pageSize) => Paging(filterExpression, m => m.CreateTime, SortOrder.Descending, pageIndex, pageSize);
        public (List<T> data, PageModel pageInfo) Paging(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pageIndex, int pageSize)
        {
            List<T> result = new();
            int dataCount = 0;
            UnitOfWork.OperationDB(connection =>
            {
                IDbCommand command = connection.CreateCommand();
                RepositoryHelper.SetQueryCountCommand(command, filterExpression, TableName, UnitOfWork);
                Logger?.LogDebugTSQL(command);
                Type tType = typeof(T);
                using (IDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        dataCount = dr.GetInt32(0);
                    }
                }
                if (dataCount > 0)
                {
                    IDbCommand queryCommand = connection.CreateCommand();
                    RepositoryHelper.SetPagingCommand(queryCommand, filterExpression, orderExpression, sortOrder, pageIndex, pageSize, TableName, UnitOfWork);
                    Logger?.LogDebugTSQL(command);
                    using IDataReader queryDr = queryCommand.ExecuteReader();
                    while (queryDr.Read())
                    {
                        T? domain = RepositoryHelper.DataReaderConvertToDomain(queryDr);
                        if (domain == null) continue;
                        result.Add(domain);
                    }
                }
            });
            return (result, new PageModel(pageIndex, pageSize, dataCount));
        }
        public Task<bool> ExistingAsync(Guid id) => Task.FromResult(Existing(id));
        public Task<T> FirstAsync(Guid id) => Task.FromResult(First(id));
        public Task<T> FirstAsync(Expression<Func<T, bool>> filterExpression) => Task.FromResult(First(filterExpression));
        public Task<T?> FirstOrDefaultAsync(Guid id) => Task.FromResult(FirstOrDefault(id));
        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filterExpression) => Task.FromResult(FirstOrDefault(filterExpression));
        public Task<List<T>> GetListAsync(IQueryModel? queryModel) => Task.FromResult(GetList(queryModel));
        public Task<List<T>> GetListAsync(Expression<Func<T, bool>> filterExpression) => Task.FromResult(GetList(filterExpression));
        public Task<(List<T> data, PageModel pageInfo)> PagingAsync(IQueryModel? queryModel = null) => Task.FromResult(Paging(queryModel));
        public Task<(List<T> data, PageModel pageInfo)> PagingAsync(IQueryModel? queryModel, Expression<Func<T, object>> orderExpression, SortOrder sortOrder = SortOrder.Ascending)
            => Task.FromResult(Paging(queryModel, orderExpression, sortOrder));
        public Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filterExpression, int pageIndex, int pageSize)
            => Task.FromResult(Paging(filterExpression, pageIndex, pageSize));
        public Task<(List<T> data, PageModel pageInfo)> PagingAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> orderExpression, SortOrder sortOrder, int pageIndex, int pageSize)
            => Task.FromResult(Paging(filterExpression, orderExpression, sortOrder, pageIndex, pageSize));
        /// <summary>
        /// 获得创建表TSQL
        /// </summary>
        /// <returns></returns>
        protected abstract string GetCreateTableTSQL();
        /// <summary>
        /// 获取检查表是否存在的TSQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected abstract string GetTableExistsTSQL(string tableName);
    }
}
