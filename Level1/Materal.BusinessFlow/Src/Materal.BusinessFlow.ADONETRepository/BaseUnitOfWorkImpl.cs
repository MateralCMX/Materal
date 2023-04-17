using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.ADONETRepository.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Reflection;
using System.Text;

namespace Materal.BusinessFlow.ADONETRepository
{
    public abstract class BaseUnitOfWorkImpl : IUnitOfWork
    {
        public IServiceProvider ServiceProvider { get; }
        public readonly string ParamsPrefix;
        public readonly string FieldPrefix;
        public readonly string FieldSuffix;
        private readonly IDbConnection _connection;
        private readonly List<Func<IDbTransaction, IDbCommand?>> commands = new();
        private readonly ILogger<BaseUnitOfWorkImpl>? _logger;
        protected BaseUnitOfWorkImpl(IServiceProvider serviceProvider, IDbConnection connection, string paramsPrefix, string fieldPrefix, string fieldSuffix)
        {
            ServiceProvider = serviceProvider;
            _connection = connection;
            ParamsPrefix = paramsPrefix;
            FieldPrefix = fieldPrefix;
            FieldSuffix = fieldSuffix;
            _logger = serviceProvider.GetService<ILogger<BaseUnitOfWorkImpl>>();
        }
        public virtual void Commit()
        {
            _connection.Open();
            IDbTransaction transaction = _connection.BeginTransaction();
            try
            {
                foreach (Func<IDbTransaction, IDbCommand?> command in commands)
                {
                    IDbCommand? sqliteCommand = command.Invoke(transaction);
                    if (sqliteCommand == null) continue;
                    sqliteCommand.Transaction = transaction;
                    _logger?.LogDebugTSQL(sqliteCommand);
                    sqliteCommand.ExecuteNonQuery();
                }
                transaction.Commit();
                commands.Clear();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new BusinessFlowException("提交更改失败", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public virtual Task CommitAsync()
        {
            Commit();
            return Task.CompletedTask;
        }
        public virtual TRepository GetRepository<TRepository>()
            where TRepository : IBaseRepository => ServiceProvider.GetService<TRepository>() ?? throw new BusinessFlowException("获取仓储失败");
        public virtual void RegisterAdd<T>(T obj)
            where T : class, IBaseDomain => commands.Add(transaction => GetInsertDomainCommand(_connection, obj));
        public virtual void RegisterDelete<T>(T obj)
            where T : class, IBaseDomain => commands.Add(transaction => GetDeleteDomainCommand(_connection, obj));
        public virtual void RegisterEdit<T>(T obj)
            where T : class, IBaseDomain => commands.Add(transaction => GetEditDomainCommand(_connection, obj));
        /// <summary>
        /// 注册命令
        /// </summary>
        /// <param name="addFunc"></param>
        public virtual void RegisterCommand(Func<IDbConnection, IDbTransaction, IDbCommand?> addFunc) => commands.Add(transaction => addFunc(_connection, transaction));
        public virtual void Dispose()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
            _connection.Dispose();
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="queryAction"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        public virtual void OperationDB(Action<IDbConnection> queryAction)
        {
            _connection.Open();
            try
            {
                queryAction(_connection);
            }
            catch (Exception ex)
            {
                throw new BusinessFlowException("查询失败", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="queryFunc"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        public virtual async Task OperationDBAsync(Func<IDbConnection, Task> queryFunc)
        {
            _connection.Open();
            try
            {
                await queryFunc(_connection);
            }
            catch (Exception ex)
            {
                throw new BusinessFlowException("查询失败", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        /// <summary>
        /// 获得新增实体命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual IDbCommand GetInsertDomainCommand<T>(IDbConnection connection, T obj, string? tableName = null)
            where T : class, IBaseDomain
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
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="obj"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual IDbCommand GetEditDomainCommand<T>(IDbConnection connection, T obj, string? tableName = null)
            where T : class, IBaseDomain
        {
            Type tType = obj.GetType();
            tableName ??= tType.Name;
            List<string> propertyNames = new();
            string primaryKeyParameterName = $"@{nameof(IBaseDomain.ID)}";
            IDbCommand command = connection.CreateCommand();
            command.AddParameter(primaryKeyParameterName, obj.ID);
            foreach (PropertyInfo propertyInfo in tType.GetProperties())
            {
                if (propertyInfo.Name == nameof(IBaseDomain.ID)) continue;
                string parameterName = $"@{propertyInfo.Name}";
                propertyNames.Add($"{GetTSQLField(propertyInfo.Name)}={parameterName}");
                object value = propertyInfo.GetValue(obj) ?? DBNull.Value;
                command.AddParameter(parameterName, value);
            }
            StringBuilder tSql = new();
            tSql.AppendLine($"UPDATE {GetTSQLField(tableName)}");
            tSql.AppendLine($"SET {string.Join(",", propertyNames)}");
            tSql.AppendLine($"WHERE {GetTSQLField(nameof(IBaseDomain.ID))}={primaryKeyParameterName}");
            command.CommandText = tSql.ToString();
            return command;
        }
        /// <summary>
        /// 获得删除实体命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual IDbCommand GetDeleteDomainCommand<T>(IDbConnection connection, T obj, string? tableName = null)
            where T : class, IBaseDomain
        {
            Type tType = obj.GetType();
            tableName ??= tType.Name;
            IDbCommand command = connection.CreateCommand();
            string primaryKeyParameterName = $"@{nameof(IBaseDomain.ID)}";
            command.AddParameter(primaryKeyParameterName, obj.ID);
            StringBuilder tSql = new();
            tSql.AppendLine($"DELETE FROM {GetTSQLField(tableName)}");
            tSql.AppendLine($"WHERE {GetTSQLField(nameof(IBaseDomain.ID))}={primaryKeyParameterName}");
            command.CommandText = tSql.ToString();
            return command;
        }
        public virtual string GetTSQLField(string field) => $"{FieldPrefix}{field}{FieldSuffix}";
    }
}
