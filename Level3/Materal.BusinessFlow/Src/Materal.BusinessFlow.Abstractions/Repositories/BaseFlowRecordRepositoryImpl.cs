using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Enums;
using Materal.BusinessFlow.Abstractions.Models;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.TTA.ADONETRepository;
using Materal.TTA.ADONETRepository.Extensions;
using Materal.TTA.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace Materal.BusinessFlow.Abstractions.Repositories
{
    public abstract class BaseFlowRecordRepositoryImpl : IFlowRecordRepository
    {
        protected readonly IRepositoryHelper<FlowRecordDTO, Guid> ViewRepositoryHelper;
        protected readonly IRepositoryHelper<FlowRecord, Guid> RepositoryHelper;
        protected readonly IBusinessFlowUnitOfWork UnitOfWork;
        protected readonly ILogger? Logger;
        protected BaseFlowRecordRepositoryImpl(IBusinessFlowUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ViewRepositoryHelper = UnitOfWork.ServiceProvider.GetRequiredService<IRepositoryHelper<FlowRecordDTO, Guid>>();
            RepositoryHelper = UnitOfWork.ServiceProvider.GetRequiredService<IRepositoryHelper<FlowRecord, Guid>>();
            ILoggerFactory? loggerFactory = UnitOfWork.ServiceProvider.GetService<ILoggerFactory>();
            Logger = loggerFactory?.CreateLogger(GetType());
        }
        public virtual Guid Add(Guid flowTemplateID, FlowRecord domain)
        {
            domain.ID = Guid.NewGuid();
            domain.State = FlowRecordStateEnum.Wait;
            string tableName = GetTableName(flowTemplateID);
            UnitOfWork.RegisterCommand((connection, transaction) => UnitOfWork.GetInsertDomainCommand<FlowRecord, Guid>(connection, domain, tableName));
            return domain.ID;
        }
        public virtual void Edit(Guid flowTemplateID, FlowRecord domain)
        {
            string tableName = GetTableName(flowTemplateID);
            UnitOfWork.RegisterCommand((connection, transaction) => UnitOfWork.GetEditDomainCommand<FlowRecord, Guid>(connection, domain, tableName));
        }
        public virtual void Init(Guid flowTemplateID)
        {
            UnitOfWork.OperationDB(connection =>
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = GetTableExistsTSQL(flowTemplateID);
                using (IDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr.GetInt32(0) > 0) return;
                    }
                }
                IDbCommand createTableCommand = connection.CreateCommand();
                createTableCommand.CommandText = GetCreateTableTSQL(flowTemplateID);
                createTableCommand.ExecuteNonQuery();
            });
        }
        public virtual FlowRecord First(Guid flowTemplateID, Guid id) => FirstOrDefault(flowTemplateID, id) ?? throw new BusinessFlowException("数据不存在");
        public virtual FlowRecord First(Guid flowTemplateID, Expression<Func<FlowRecord, bool>> expression) => FirstOrDefault(flowTemplateID, expression) ?? throw new BusinessFlowException("数据不存在");
        public virtual FlowRecord? FirstOrDefault(Guid flowTemplateID, Guid id) => FirstOrDefault(flowTemplateID, m => m.ID == id);
        public virtual FlowRecord? FirstOrDefault(Guid flowTemplateID, Expression<Func<FlowRecord, bool>> expression)
        {
            List<FlowRecord> domains = GetList(flowTemplateID, expression);
            return domains.FirstOrDefault();
        }
        public virtual int GetMaxSortIndex(Guid flowTemplateID, Guid flowID)
        {
            int index = 0;
            string tableName = GetTableName(flowTemplateID);
            UnitOfWork.OperationDB(connection =>
            {
                IDbCommand sqliteCommand = connection.CreateCommand();
                sqliteCommand.CommandText = $"SELECT {RepositoryHelper.GetIsNullTSQL("MAX(SortIndex)", "-1")} From {tableName} where FlowID={UnitOfWork.GetParams("FlowID")}";
                sqliteCommand.AddParameter(UnitOfWork.GetParams("FlowID"), flowID);
                using IDataReader dr = sqliteCommand.ExecuteReader();
                while (dr.Read())
                {
                    index = dr.GetInt32(0) + 1;
                }
            });
            return index;
        }
        public virtual List<FlowRecordDTO> GetDTOList(QueryFlowRecordDTOModel queryModel)
        {
            Expression<Func<FlowRecordDTO, bool>> expression = queryModel.GetSearchExpression<FlowRecordDTO>();
            return GetDTOList(queryModel.FlowTemplateID, expression);
        }
        private void ExcuteCommand(Guid flowTemplateID, IDbConnection connection, Action action)
        {
            IDbCommand existCommand = connection.CreateCommand();
            existCommand.CommandText = GetTableExistsTSQL(flowTemplateID);
            using IDataReader existDr = existCommand.ExecuteReader();
            while (existDr.Read())
            {
                if (existDr.GetInt32(0) <= 0) break;
                action();
            }
        }
        public virtual List<FlowRecordDTO> GetDTOList(Guid flowTemplateID, Expression<Func<FlowRecordDTO, bool>> filterExpression)
        {
            List<FlowRecordDTO> result = new();
            UnitOfWork.OperationDB(connection =>
            {
                ExcuteCommand(flowTemplateID, connection, () =>
                {
                    string tableName = GetTableName(flowTemplateID);
                    IDbCommand command = connection.CreateCommand();
                    SetQueryViewCommand(command, flowTemplateID, filterExpression);
                    using IDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        FlowRecordDTO? dto = ViewRepositoryHelper.DataReaderConvertToDomain(dr);
                        if (dto == null) continue;
                        result.Add(dto);
                    }
                });
            });
            return result;
        }
        public virtual List<FlowRecord> GetList(QueryFlowRecordModel queryModel)
        {
            Expression<Func<FlowRecord, bool>> expression = queryModel.GetSearchExpression<FlowRecord>();
            return GetList(queryModel.FlowTemplateID, expression);
        }
        public virtual List<FlowRecord> GetList(Guid flowTemplateID, Expression<Func<FlowRecord, bool>> filterExpression)
        {
            List<FlowRecord> result = new();
            UnitOfWork.OperationDB(connection =>
            {
                ExcuteCommand(flowTemplateID, connection, () =>
                {
                    string tableName = GetTableName(flowTemplateID);
                    IDbCommand command = connection.CreateCommand();
                    SetQueryCommand(command, flowTemplateID, filterExpression, m => m.CreateTime, SortOrderEnum.Descending);
                    using IDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        FlowRecord? dto = RepositoryHelper.DataReaderConvertToDomain(dr);
                        if (dto == null) continue;
                        result.Add(dto);
                    }
                });
            });
            return result;
        }
        public virtual Task InitAsync(Guid flowTemplateID)
        {
            Init(flowTemplateID);
            return Task.CompletedTask;
        }
        public virtual Task<FlowRecord> FirstAsync(Guid flowTemplateID, Guid id) => Task.FromResult(First(flowTemplateID, id));
        public virtual Task<FlowRecord> FirstAsync(Guid flowTemplateID, Expression<Func<FlowRecord, bool>> expression) => Task.FromResult(First(flowTemplateID, expression));
        public virtual Task<FlowRecord?> FirstOrDefaultAsync(Guid flowTemplateID, Guid id) => Task.FromResult(FirstOrDefault(flowTemplateID, id));
        public virtual Task<FlowRecord?> FirstOrDefaultAsync(Guid flowTemplateID, Expression<Func<FlowRecord, bool>> expression) => Task.FromResult(FirstOrDefault(flowTemplateID, expression));
        public virtual Task<int> GetMaxSortIndexAsync(Guid flowTemplateID, Guid flowID) => Task.FromResult(GetMaxSortIndex(flowTemplateID, flowID));
        public virtual Task<List<FlowRecord>> GetListAsync(QueryFlowRecordModel queryModel) => Task.FromResult(GetList(queryModel));
        public virtual Task<List<FlowRecord>> GetListAsync(Guid flowTemplateID, Expression<Func<FlowRecord, bool>> filterExpression)
            => Task.FromResult(GetList(flowTemplateID, filterExpression));
        public virtual Task<List<FlowRecordDTO>> GetDTOListAsync(QueryFlowRecordDTOModel queryModel) => Task.FromResult(GetDTOList(queryModel));
        public virtual Task<List<FlowRecordDTO>> GetDTOListAsync(Guid flowTemplateID, Expression<Func<FlowRecordDTO, bool>> filterExpression)
            => Task.FromResult(GetDTOList(flowTemplateID, filterExpression));
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
        protected abstract string GetCreateTableTSQL(Guid flowTemplateID);
        /// <summary>
        /// 获得表名
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <returns></returns>
        protected virtual string GetTableName(Guid flowTemplateID) => $"{nameof(FlowRecord)}_{flowTemplateID.ToString().Replace("-", "")}";
        #region 私有方法
        /// <summary>
        /// 设置查询命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="flowTemplateID"></param>
        /// <param name="expression"></param>
        private void SetQueryCommand(IDbCommand command, Guid flowTemplateID, Expression<Func<FlowRecord, bool>> expression, Expression<Func<FlowRecord, object>> orderExpression, SortOrderEnum sortOrder)
        {
            string tableName = GetTableName(flowTemplateID);
            RepositoryHelper.SetQueryCommand(command, expression, orderExpression, sortOrder, tableName);
        }
        /// <summary>
        /// 设置查询视图命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="tableName"></param>
        /// <param name="filterExpression"></param>
        private void SetQueryViewCommand(IDbCommand command, string tableName, Expression<Func<FlowRecordDTO, bool>> filterExpression)
        {
            StringBuilder tSql = new();
            tSql.AppendLine(@$"SELECT
	{UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.ID))},
	{UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.FlowID))},
	{UnitOfWork.GetTSQLField(nameof(Step))}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.FlowTemplateID))},
	{UnitOfWork.GetTSQLField(nameof(FlowTemplate))}.{UnitOfWork.GetTSQLField(nameof(FlowTemplate.Name))} AS {UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.FlowTemplateName))},
	{UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.StepID))},
	{UnitOfWork.GetTSQLField(nameof(Step))}.{UnitOfWork.GetTSQLField(nameof(Step.Name))} AS {UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.StepName))},
	{UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.NodeID))},
	{UnitOfWork.GetTSQLField(nameof(Node))}.{UnitOfWork.GetTSQLField(nameof(Node.Name))} AS {UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.NodeName))},
	{UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.UserID))},
	{UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.OperationUserID))},
	{UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.State))},
	{UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.SortIndex))},
    {UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.NodeHandleType))},
	{UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.Data))},
	{UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.ResultMessage))},
	{UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.CreateTime))}
FROM {UnitOfWork.GetTSQLField(tableName)}
	INNER JOIN {UnitOfWork.GetTSQLField(nameof(Step))} ON {UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.StepID))} = {UnitOfWork.GetTSQLField(nameof(Step))}.{UnitOfWork.GetTSQLField(nameof(Step.ID))} 
	INNER JOIN {UnitOfWork.GetTSQLField(nameof(FlowTemplate))} ON {UnitOfWork.GetTSQLField(nameof(FlowTemplate))}.{UnitOfWork.GetTSQLField(nameof(FlowTemplate.ID))} = {UnitOfWork.GetTSQLField(nameof(Step))}.{UnitOfWork.GetTSQLField(nameof(Step.FlowTemplateID))}
	INNER JOIN {UnitOfWork.GetTSQLField(nameof(Node))} ON {UnitOfWork.GetTSQLField(tableName)}.{UnitOfWork.GetTSQLField(nameof(FlowRecordDTO.NodeID))} = {UnitOfWork.GetTSQLField(nameof(Node))}.{UnitOfWork.GetTSQLField(nameof(Node.ID))}");
            string whereTSQLs = ViewRepositoryHelper.ExpressionToTSQL(command, filterExpression, null);
            if (!string.IsNullOrWhiteSpace(whereTSQLs))
            {
                tSql.AppendLine($"WHERE {whereTSQLs}");
            }
            command.CommandText = tSql.ToString();
        }
        /// <summary>
        /// 设置查询视图命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="flowTemplateID"></param>
        /// <param name="filterExpression"></param>
        private void SetQueryViewCommand(IDbCommand command, Guid flowTemplateID, Expression<Func<FlowRecordDTO, bool>> filterExpression)
        {
            string tableName = GetTableName(flowTemplateID);
            SetQueryViewCommand(command, tableName, filterExpression);
        }
        #endregion
    }
}
