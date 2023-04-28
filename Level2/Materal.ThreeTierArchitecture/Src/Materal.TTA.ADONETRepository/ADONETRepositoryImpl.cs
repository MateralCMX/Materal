﻿using Materal.TTA.ADONETRepository.Extensions;
using Materal.TTA.Common;
using Materal.Utils.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// ADONET仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public abstract class ADONETRepositoryImpl<TEntity, TPrimaryKeyType> : CommonRepositoryImpl<TEntity, TPrimaryKeyType>, IADONETRepository<TEntity, TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        protected readonly IADONETUnitOfWork UnitOfWork;
        /// <summary>
        /// 日志对象
        /// </summary>
        protected readonly ILogger? Logger;
        /// <summary>
        /// 仓储帮助
        /// </summary>
        protected readonly IRepositoryHelper<TEntity, TPrimaryKeyType> RepositoryHelper;
        /// <summary>
        /// 表名
        /// </summary>
        protected virtual string TableName => typeof(TEntity).Name;
        private static bool _isFirst = true;
        private static readonly object _initLockObject = new();
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ADONETRepositoryImpl(IADONETUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ILoggerFactory? loggerFactory = UnitOfWork.ServiceProvider.GetService<ILoggerFactory>();
            RepositoryHelper = UnitOfWork.ServiceProvider.GetService<IRepositoryHelper<TEntity, TPrimaryKeyType>>() ?? throw new TTAException("未找到仓储帮助类");
            Logger = loggerFactory?.CreateLogger(GetType());
            InitDB();
        }
        /// <summary>
        /// 初始化
        /// </summary>
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
        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override int Count(Expression<Func<TEntity, bool>> expression)
        {
            int result = 0;
            UnitOfWork.OperationDB(connection =>
            {
                IDbCommand command = connection.CreateCommand();
                RepositoryHelper.SetQueryCountCommand(command, expression, TableName, UnitOfWork);
                Logger?.LogDebugTSQL(command);
                Type tType = typeof(TEntity);
                using IDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    result = dr.GetInt32(0);
                }
            });
            return result;
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public override List<TEntity> Find(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderExpression, SortOrder sortOrder)
        {
            List<TEntity> result = new();
            UnitOfWork.OperationDB(connection =>
            {
                IDbCommand command = connection.CreateCommand();
                RepositoryHelper.SetQueryCommand(command, expression, orderExpression, sortOrder, TableName, UnitOfWork);
                Logger?.LogDebugTSQL(command);
                Type tType = typeof(TEntity);
                using IDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    TEntity? domain = RepositoryHelper.DataReaderConvertToDomain(dr);
                    if (domain == null) continue;
                    result.Add(domain);
                }
            });
            return result;
        }
        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            TEntity? result = null;
            UnitOfWork.OperationDB(connection =>
            {
                IDbCommand command = connection.CreateCommand();
                RepositoryHelper.SetQueryOneRowCommand(command, expression, m => m.ID, SortOrder.Descending, TableName, UnitOfWork);
                Logger?.LogDebugTSQL(command);
                Type tType = typeof(TEntity);
                using IDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    result = RepositoryHelper.DataReaderConvertToDomain(dr);
                }
            });
            return result;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="orderExpression"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public override (List<TEntity> data, PageModel pageInfo) Paging(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> orderExpression, SortOrder sortOrder, int pageIndex, int pageSize)
        {
            List<TEntity> result = new();
            int dataCount = 0;
            UnitOfWork.OperationDB(connection =>
            {
                IDbCommand command = connection.CreateCommand();
                RepositoryHelper.SetQueryCountCommand(command, filterExpression, TableName, UnitOfWork);
                Logger?.LogDebugTSQL(command);
                Type tType = typeof(TEntity);
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
                        TEntity? domain = RepositoryHelper.DataReaderConvertToDomain(queryDr);
                        if (domain == null) continue;
                        result.Add(domain);
                    }
                }
            });
            return (result, new PageModel(pageIndex, pageSize, dataCount));
        }
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