using Materal.TTA.ADONETRepository.Extensions;
using Materal.TTA.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Reflection;
using System.Text;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// ADONET工作单元
    /// </summary>
    public abstract class ADONETUnitOfWorkImpl<TDBOption> : IADONETUnitOfWork, IDisposable
        where TDBOption : DBOption
    {
        /// <summary>
        /// 服务容器
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }
        private readonly IDbConnection _connection;
        private readonly List<Func<IDbTransaction, IDbCommand?>> _commands = new();
        private readonly ILogger<ADONETUnitOfWorkImpl<TDBOption>>? _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected ADONETUnitOfWorkImpl(IServiceProvider serviceProvider, IDbConnection connection)
        {
            ServiceProvider = serviceProvider;
            _connection = connection;
            _logger = serviceProvider.GetService<ILogger<ADONETUnitOfWorkImpl<TDBOption>>>();
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <exception cref="TTAException"></exception>
        public virtual void Commit()
        {
            _connection.Open();
            IDbTransaction transaction = _connection.BeginTransaction();
            try
            {
                foreach (Func<IDbTransaction, IDbCommand?> command in _commands)
                {
                    IDbCommand? sqliteCommand = command.Invoke(transaction);
                    if (sqliteCommand == null) continue;
                    sqliteCommand.Transaction = transaction;
                    _logger?.LogTSQL(sqliteCommand);
                    sqliteCommand.ExecuteNonQuery();
                }
                transaction.Commit();
                _commands.Clear();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new TTAException("提交更改失败", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        public virtual Task CommitAsync()
        {
            Commit();
            return Task.CompletedTask;
        }
        /// <summary>
        /// 获得仓储
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public virtual TRepository GetRepository<TRepository>()
            where TRepository : IRepository => ServiceProvider.GetService<TRepository>() ?? throw new TTAException("获取仓储失败");
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        public virtual void RegisterAdd<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct => _commands.Add(transaction => GetInsertDomainCommand<TEntity, TPrimaryKeyType>(_connection, obj));
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool TryRegisterAdd<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            RegisterAdd<TEntity, TPrimaryKeyType>(obj);
            return true;
        }
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        public virtual void RegisterEdit<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct => _commands.Add(transaction => GetEditDomainCommand<TEntity, TPrimaryKeyType>(_connection, obj));
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool TryRegisterEdit<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            RegisterEdit<TEntity, TPrimaryKeyType>(obj);
            return true;
        }
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        public virtual void RegisterDelete<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct => _commands.Add(transaction => GetDeleteDomainCommand<TEntity, TPrimaryKeyType>(_connection, obj));
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool TryRegisterDelete<TEntity, TPrimaryKeyType>(TEntity obj)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            RegisterDelete<TEntity, TPrimaryKeyType>(obj);
            return true;
        }
        /// <summary>
        /// 注册命令
        /// </summary>
        /// <param name="addFunc"></param>
        public virtual void RegisterCommand(Func<IDbConnection, IDbTransaction, IDbCommand?> addFunc) => _commands.Add(transaction => addFunc(_connection, transaction));
        /// <summary>
        /// 操作数据库
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public virtual void OperationDB(Action<IDbConnection> action)
        {
            _connection.Open();
            try
            {
                action(_connection);
            }
            catch (Exception ex)
            {
                throw new TTAException("执行失败", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        /// <summary>
        /// 操作数据库
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        public virtual async Task OperationDBAsync(Func<IDbConnection, Task> func)
        {
            _connection.Open();
            try
            {
                await func(_connection);
            }
            catch (Exception ex)
            {
                throw new TTAException("执行失败", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        /// <summary>
        /// 获得新增实体命令
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="connection"></param>
        /// <param name="obj"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual IDbCommand GetInsertDomainCommand<TEntity, TPrimaryKeyType>(IDbConnection connection, TEntity obj, string? tableName = null)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            Type tType = obj.GetType();
            tableName ??= tType.Name;
            List<string> propertyNames = new();
            List<string> parametersNames = new();
            IDbCommand command = connection.CreateCommand();
            foreach (PropertyInfo propertyInfo in tType.GetProperties())
            {
                object value = propertyInfo.GetValue(obj) ?? DBNull.Value;
                propertyNames.Add(GetTSQLField(propertyInfo.Name));
                string parameterName = $"@{propertyInfo.Name}";
                parametersNames.Add(parameterName);
                command.AddParameter(parameterName, value);
            }
            StringBuilder tSql = new();
            tSql.AppendLine($"INSERT INTO {GetTSQLField(tableName)}({string.Join(",", propertyNames)})");
            tSql.AppendLine($"VALUES({string.Join(",", parametersNames)})");
            command.CommandText = tSql.ToString();
            return command;
        }
        /// <summary>
        /// 获得修改实体命令
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="connection"></param>
        /// <param name="obj"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual IDbCommand GetEditDomainCommand<TEntity, TPrimaryKeyType>(IDbConnection connection, TEntity obj, string? tableName = null)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            Type tType = obj.GetType();
            tableName ??= tType.Name;
            List<string> propertyNames = new();
            string primaryKeyParameterName = $"@{nameof(IEntity<TPrimaryKeyType>.ID)}";
            IDbCommand command = connection.CreateCommand();
            command.AddParameter(primaryKeyParameterName, obj.ID);
            foreach (PropertyInfo propertyInfo in tType.GetProperties())
            {
                if (propertyInfo.Name == nameof(IEntity<TPrimaryKeyType>.ID)) continue;
                string parameterName = $"@{propertyInfo.Name}";
                propertyNames.Add($"{GetTSQLField(propertyInfo.Name)}={parameterName}");
                object value = propertyInfo.GetValue(obj) ?? DBNull.Value;
                command.AddParameter(parameterName, value);
            }
            StringBuilder tSql = new();
            tSql.AppendLine($"UPDATE {GetTSQLField(tableName)}");
            tSql.AppendLine($"SET {string.Join(",", propertyNames)}");
            tSql.AppendLine($"WHERE {GetTSQLField(nameof(IEntity<TPrimaryKeyType>.ID))}={primaryKeyParameterName}");
            command.CommandText = tSql.ToString();
            return command;
        }
        /// <summary>
        /// 获得删除实体命令
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="connection"></param>
        /// <param name="obj"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual IDbCommand GetDeleteDomainCommand<TEntity, TPrimaryKeyType>(IDbConnection connection, TEntity obj, string? tableName = null)
            where TEntity : class, IEntity<TPrimaryKeyType>
            where TPrimaryKeyType : struct
        {
            Type tType = obj.GetType();
            tableName ??= tType.Name;
            IDbCommand command = connection.CreateCommand();
            string primaryKeyParameterName = $"@{nameof(IEntity<TPrimaryKeyType>.ID)}";
            command.AddParameter(primaryKeyParameterName, obj.ID);
            StringBuilder tSql = new();
            tSql.AppendLine($"DELETE FROM {GetTSQLField(tableName)}");
            tSql.AppendLine($"WHERE {GetTSQLField(nameof(IEntity<TPrimaryKeyType>.ID))}={primaryKeyParameterName}");
            command.CommandText = tSql.ToString();
            return command;
        }
        /// <summary>
        /// 获得TSQL字段
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public abstract string GetTSQLField(string field);
        /// <summary>
        /// 释放
        /// </summary>
        public virtual void Dispose()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
            _connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
    /// <summary>
    /// ADONET工作单元
    /// </summary>
    public abstract class ADONETUnitOfWorkImpl<TDbConfig, TPrimaryKeyType> : ADONETUnitOfWorkImpl<DBOption>, IADONETUnitOfWork<TPrimaryKeyType>
        where TDbConfig : DBOption
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        protected ADONETUnitOfWorkImpl(IServiceProvider serviceProvider, IDbConnection connection) : base(serviceProvider, connection)
        {
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void RegisterAdd<T>(T obj)
            where T : class, IEntity<TPrimaryKeyType>
        {
            RegisterAdd<T,TPrimaryKeyType>(obj);
        }
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void RegisterDelete<T>(T obj)
            where T : class, IEntity<TPrimaryKeyType>
        {
            RegisterDelete<T, TPrimaryKeyType>(obj);
        }
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void RegisterEdit<T>(T obj)
            where T : class, IEntity<TPrimaryKeyType>
        {
            RegisterEdit<T, TPrimaryKeyType>(obj);
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool TryRegisterAdd<T>(T obj)
            where T : class, IEntity<TPrimaryKeyType>
        {
            return TryRegisterAdd<T, TPrimaryKeyType>(obj);
        }
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool TryRegisterDelete<T>(T obj)
            where T : class, IEntity<TPrimaryKeyType>
        {
            return TryRegisterDelete<T, TPrimaryKeyType>(obj);
        }
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool TryRegisterEdit<T>(T obj)
            where T : class, IEntity<TPrimaryKeyType>
        {
            return TryRegisterEdit<T, TPrimaryKeyType>(obj);
        }
    }
}
