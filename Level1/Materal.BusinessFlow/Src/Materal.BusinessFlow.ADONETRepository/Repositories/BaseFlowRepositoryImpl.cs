using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Enums;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.ADONETRepository.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Text;

namespace Materal.BusinessFlow.ADONETRepository.Repositories
{
    public abstract class BaseFlowRepositoryImpl : IFlowRepository
    {
        protected readonly BaseUnitOfWorkImpl UnitOfWork;
        protected readonly ILogger? Logger;
        protected BaseFlowRepositoryImpl(IUnitOfWork unitOfWork)
        {
            if (unitOfWork is not BaseUnitOfWorkImpl unitOfWorkImpl) throw new BusinessFlowException("工作单元类型错误");
            UnitOfWork = unitOfWorkImpl;
            ILoggerFactory? loggerFactory = unitOfWorkImpl.ServiceProvider.GetService<ILoggerFactory>();
            Logger = loggerFactory?.CreateLogger(GetType());
        }
        public virtual void Init(FlowTemplate flowTemplate)
        {
            UnitOfWork.OperationDB(connection =>
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = GetTableExistsTSQL(flowTemplate.ID);
                using (IDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr.GetInt32(0) > 0) return;
                    }
                }
                IDbCommand createTableCommand = connection.CreateCommand();
                createTableCommand.CommandText = GetCreateTableTSQL(flowTemplate, connection.CreateCommand());
                createTableCommand.ExecuteNonQuery();
            });
        }
        public virtual void AddTableField(FlowTemplate flowTemplate, DataModelField dataModelField)
        {
            UnitOfWork.RegisterCommand((connection, transaction) =>
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = GetTableExistsTSQL(flowTemplate.ID);
                command.Transaction = transaction;
                using (IDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr.GetInt32(0) <= 0) return null;
                    }
                }
                IDbCommand createTableCommand = connection.CreateCommand();
                createTableCommand.CommandText = GetAddTableFieldTSQL(flowTemplate, dataModelField);
                return createTableCommand;
            });
        }
        public virtual void EditTableField(FlowTemplate flowTemplate, DataModelField oldDataModelField, DataModelField newDataModelField)
        {
            UnitOfWork.RegisterCommand((connection, transaction) =>
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = GetTableExistsTSQL(flowTemplate.ID);
                command.Transaction = transaction;
                using (IDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr.GetInt32(0) <= 0) return null;
                    }
                }
                IDbCommand createTableCommand = connection.CreateCommand();
                createTableCommand.CommandText = GetEditTableFieldTSQL(flowTemplate, oldDataModelField, newDataModelField, connection.CreateCommand());
                return createTableCommand;
            });
        }
        public virtual void DeleteTableField(FlowTemplate flowTemplate, DataModelField dataModelField)
        {
            UnitOfWork.RegisterCommand((connection, transaction) =>
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = GetTableExistsTSQL(flowTemplate.ID);
                command.Transaction = transaction;
                using (IDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr.GetInt32(0) <= 0) return null;
                    }
                }
                IDbCommand createTableCommand = connection.CreateCommand();
                createTableCommand.CommandText = GetDeleteTableFieldTSQL(flowTemplate, dataModelField);
                return createTableCommand;
            });
        }
        public virtual Guid Add(Guid flowTemplateID, Guid startStepID, Guid initiatorID)
        {
            Guid result = Guid.NewGuid();
            UnitOfWork.RegisterCommand((connection, transaction) =>
            {
                IDbCommand command = connection.CreateCommand();
                string tableName = GetTableName(flowTemplateID);
                List<string> fields = new()
                {
                    "ID", "FlowTemplateID", "StartStepID", "StepID", "State", "InitiatorID", "CreateTime"
                };
                List<string> paramNames = fields.Select(m => $"{UnitOfWork.ParamsPrefix}{m}").ToList();
                fields = fields.Select(m => UnitOfWork.GetTSQLField(m)).ToList();
                command.CommandText = @$"INSERT INTO {UnitOfWork.GetTSQLField(tableName)}({string.Join(",", fields)})
VALUES({string.Join(",", paramNames)})";
                command.AddParameter($"{UnitOfWork.ParamsPrefix}ID", result);
                command.AddParameter($"{UnitOfWork.ParamsPrefix}FlowTemplateID", flowTemplateID);
                command.AddParameter($"{UnitOfWork.ParamsPrefix}StartStepID", startStepID);
                command.AddParameter($"{UnitOfWork.ParamsPrefix}StepID", startStepID);
                command.AddParameter($"{UnitOfWork.ParamsPrefix}State", FlowStateEnum.Running);
                command.AddParameter($"{UnitOfWork.ParamsPrefix}InitiatorID", initiatorID);
                command.AddParameter($"{UnitOfWork.ParamsPrefix}CreateTime", DateTime.Now);
                return command;
            });
            return result;
        }
        public virtual void ComplateFlow(Guid flowTemplateID, Guid flowID)
        {
            UnitOfWork.RegisterCommand((connection, transaction) =>
            {
                IDbCommand command = connection.CreateCommand();
                string tableName = GetTableName(flowTemplateID);
                command.CommandText = @$"UPDATE {UnitOfWork.GetTSQLField(tableName)}
SET {UnitOfWork.GetTSQLField("State")}={UnitOfWork.ParamsPrefix}State
WHERE {UnitOfWork.GetTSQLField("ID")}={UnitOfWork.ParamsPrefix}ID";
                command.AddParameter($"{UnitOfWork.ParamsPrefix}ID", flowID);
                command.AddParameter($"{UnitOfWork.ParamsPrefix}State", FlowStateEnum.End);
                return command;
            });
        }
        public virtual void SetStep(Guid flowTemplateID, Guid flowID, Guid stepID)
        {
            UnitOfWork.RegisterCommand((connection, transaction) =>
            {
                IDbCommand command = connection.CreateCommand();
                string tableName = GetTableName(flowTemplateID);
                command.CommandText = @$"UPDATE {UnitOfWork.GetTSQLField(tableName)}
SET {UnitOfWork.GetTSQLField("StepID")}={UnitOfWork.ParamsPrefix}StepID
WHERE {UnitOfWork.GetTSQLField("ID")}={UnitOfWork.ParamsPrefix}ID";
                command.AddParameter($"{UnitOfWork.ParamsPrefix}ID", flowID);
                command.AddParameter($"{UnitOfWork.ParamsPrefix}StepID", stepID);
                return command;
            });
        }
        public virtual void SaveData(Guid flowTemplateID, Guid flowID, Dictionary<string, object?> datas)
        {
            if (datas.Count <= 0) return;
            UnitOfWork.RegisterCommand((connection, transaction) =>
            {
                IDbCommand command = connection.CreateCommand();
                string tableName = GetTableName(flowTemplateID);
                StringBuilder tsql = new();
                tsql.AppendLine($"UPDATE {UnitOfWork.GetTSQLField(tableName)}");
                List<string> paramsNames = new();
                command.AddParameter($"{UnitOfWork.ParamsPrefix}ID", flowID);
                foreach (KeyValuePair<string, object?> data in datas)
                {
                    string paraName = $"{UnitOfWork.ParamsPrefix}{data.Key}";
                    paramsNames.Add($"{UnitOfWork.GetTSQLField(data.Key)}={paraName}");
                    command.AddParameter($"{paraName}", data.Value);
                }
                tsql.AppendLine($"SET {string.Join(",", paramsNames)}");
                tsql.AppendLine($"WHERE {UnitOfWork.GetTSQLField("ID")}={UnitOfWork.ParamsPrefix}ID");
                command.CommandText = tsql.ToString();
                return command;
            });
        }
        public virtual Guid GetInitiatorID(Guid flowTemplateID, Guid flowID)
        {
            Guid? result = null;
            UnitOfWork.OperationDB(connection =>
            {
                IDbCommand command = connection.CreateCommand();
                string tableName = GetTableName(flowTemplateID);
                command.CommandText = @$"SELECT {UnitOfWork.GetTSQLField("InitiatorID")}
FROM {UnitOfWork.GetTSQLField(tableName)}
WHERE {UnitOfWork.GetTSQLField("FlowTemplateID")}={UnitOfWork.ParamsPrefix}FlowTemplateID AND {UnitOfWork.GetTSQLField("ID")}={UnitOfWork.ParamsPrefix}ID";
                command.AddParameter($"{UnitOfWork.ParamsPrefix}ID", flowID);
                command.AddParameter($"{UnitOfWork.ParamsPrefix}FlowTemplateID", flowTemplateID);
                Logger?.LogDebugTSQL(command);
                using IDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    result = dr.GetGuid(0);
                }
            });
            return result != null ? result.Value : throw new BusinessFlowException("数据不存在");
        }
        public virtual Dictionary<string, object?> GetData(Guid flowTemplateID, Guid flowID, List<DataModelField> dataModelFields)
        {
            Dictionary<string, object?> result = new();
            if (dataModelFields.Count <= 0) return result;
            UnitOfWork.OperationDB(connection =>
            {
                string tableName = GetTableName(flowTemplateID);
                IDbCommand command = connection.CreateCommand();
                string fields = string.Join(",", dataModelFields.Select(m => UnitOfWork.GetTSQLField(m.Name)));
                command.CommandText = @$"SELECT {fields}
FROM {UnitOfWork.GetTSQLField(tableName)}
WHERE ID={UnitOfWork.ParamsPrefix}ID";
                command.AddParameter($"{UnitOfWork.ParamsPrefix}ID", flowID);
                Logger?.LogDebugTSQL(command);
                using IDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string name = dr.GetName(i);
                        if (!dr.IsDBNull(i))
                        {
                            DataModelField dataModelField = dataModelFields.First(x => x.Name == name);
                            object value = dataModelField.DataType switch
                            {
                                DataTypeEnum.Enum or
                                DataTypeEnum.String => dr.GetString(i),
                                DataTypeEnum.Number => dr.GetDecimal(i),
                                DataTypeEnum.Date or
                                DataTypeEnum.Time or
                                DataTypeEnum.DateTime => dr.GetDateTime(i),
                                DataTypeEnum.Boole => dr.GetBoolean(i),
                                _ => throw new BusinessFlowException("未知类型"),
                            };
                            result.Add(name, dr.GetValue(i));
                        }
                        else
                        {
                            result.Add(name, null);
                        }
                    }
                }
            });
            return result;
        }
        public virtual Task InitAsync(FlowTemplate flowTemplate)
        {
            Init(flowTemplate);
            return Task.CompletedTask;
        }
        public virtual Task AddTableFieldAsync(FlowTemplate flowTemplate, DataModelField dataModelField)
        {
            AddTableField(flowTemplate, dataModelField);
            return Task.CompletedTask;
        }
        public virtual Task EditTableFieldAsync(FlowTemplate flowTemplate, DataModelField oldDataModelField, DataModelField newDataModelField)
        {
            EditTableField(flowTemplate, oldDataModelField, newDataModelField);
            return Task.CompletedTask;
        }
        public virtual Task DeleteTableFieldAsync(FlowTemplate flowTemplate, DataModelField dataModelField)
        {
            DeleteTableField(flowTemplate, dataModelField);
            return Task.CompletedTask;
        }
        public virtual Task<Dictionary<string, object?>> GetDataAsync(Guid flowTemplateID, Guid flowID, List<DataModelField> dataModelFields)
            => Task.FromResult(GetData(flowTemplateID, flowID, dataModelFields));
        public virtual Task<Guid> GetInitiatorIDAsync(Guid flowTemplateID, Guid flowID)
            => Task.FromResult(GetInitiatorID(flowTemplateID, flowID));
        /// <summary>
        /// 获取检查表是否存在的TSQL
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <returns></returns>
        protected abstract string GetTableExistsTSQL(Guid flowTemplateID);
        /// <summary>
        /// 获得创建表TSQL
        /// </summary>
        /// <param name="flowTemplate"></param>
        /// <param name="sqliteCommand"></param>
        /// <returns></returns>
        protected abstract string GetCreateTableTSQL(FlowTemplate flowTemplate, IDbCommand sqliteCommand);
        /// <summary>
        /// 获得添加表字段TSQL
        /// </summary>
        /// <param name="flowTemplate"></param>
        /// <param name="dataModelField"></param>
        /// <returns></returns>
        protected abstract string GetAddTableFieldTSQL(FlowTemplate flowTemplate, DataModelField dataModelField);
        /// <summary>
        /// 获得修改表字段TSQL
        /// </summary>
        /// <param name="flowTemplate"></param>
        /// <param name="oldDataModelField"></param>
        /// <param name="newDataModelField"></param>
        /// <param name="sqliteCommand"></param>
        /// <returns></returns>
        protected abstract string GetEditTableFieldTSQL(FlowTemplate flowTemplate, DataModelField oldDataModelField, DataModelField newDataModelField, IDbCommand sqliteCommand);
        /// <summary>
        /// 获得删除表字段TSQL
        /// </summary>
        /// <param name="flowTemplate"></param>
        /// <param name="dataModelField"></param>
        /// <returns></returns>
        protected abstract string GetDeleteTableFieldTSQL(FlowTemplate flowTemplate, DataModelField dataModelField);
        /// <summary>
        /// 获得表名
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <returns></returns>
        protected virtual string GetTableName(Guid flowTemplateID) => $"Flow_{flowTemplateID.ToString().Replace("-", "")}";
    }
}
