﻿using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.ADONETRepository.Repositories;
using Materal.TTA.ADONETRepository.Extensions;
using Materal.TTA.SqlServerADONETRepository;
using System.Data;
using System.Text;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class FlowRepositoryImpl : BaseFlowRepositoryImpl, IFlowRepository
    {
        public FlowRepositoryImpl(IBusinessFlowUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        protected override string GetAddTableFieldTSQL(FlowTemplate flowTemplate, DataModelField dataModelField)
        {
            string tableName = GetTableName(flowTemplate.ID);
            StringBuilder tSqlBuilder = new();
            string type = GetDataTypeByDataTypeEnum(dataModelField.DataType);
            tSqlBuilder.AppendLine($"ALTER TABLE {UnitOfWork.GetTSQLField(tableName)} ADD {UnitOfWork.GetTSQLField(dataModelField.Name)} {type} NULL");
            string result = tSqlBuilder.ToString();
            return result;
        }
        protected override string GetCreateTableTSQL(FlowTemplate flowTemplate, IDbCommand sqliteCommand)
        {
            string tableName = GetTableName(flowTemplate.ID);
            StringBuilder tSqlBuilder = new();
            tSqlBuilder.AppendLine($"CREATE TABLE {UnitOfWork.GetTSQLField(tableName)}(");
            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField(nameof(IDomain.ID))} {UnitOfWork.GetTSQLField("uniqueidentifier")} NOT NULL,");
            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField("FlowTemplateID")} {UnitOfWork.GetTSQLField("uniqueidentifier")} NOT NULL,");
            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField("StartStepID")} {UnitOfWork.GetTSQLField("uniqueidentifier")} NOT NULL,");
            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField("StepID")} {UnitOfWork.GetTSQLField("uniqueidentifier")} NOT NULL,");
            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField("State")} {UnitOfWork.GetTSQLField("tinyint")} NOT NULL,");
            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField("InitiatorID")} {UnitOfWork.GetTSQLField("uniqueidentifier")} NOT NULL,");

            sqliteCommand.AddParameter(UnitOfWork.GetParams(nameof(DataModelField.DataModelID)), flowTemplate.DataModelID);
            sqliteCommand.CommandText = @$"SELECT {UnitOfWork.GetTSQLField(nameof(DataModelField.Name))},{UnitOfWork.GetTSQLField(nameof(DataModelField.DataType))}
FROM {UnitOfWork.GetTSQLField(nameof(DataModelField))}
WHERE {UnitOfWork.GetTSQLField(nameof(DataModelField.DataModelID))} = {UnitOfWork.GetParams(nameof(DataModelField.DataModelID))}";
            using IDataReader dr = sqliteCommand.ExecuteReader();
            while (dr.Read())
            {
                string dataName = dr.GetString(0);
                DataTypeEnum dataType = (DataTypeEnum)dr.GetByte(1);
                string type = GetDataTypeByDataTypeEnum(dataType);
                tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField(dataName)} {type} NULL,");
            }

            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField(nameof(IDomain.CreateTime))} {UnitOfWork.GetTSQLField("datetime2")} NOT NULL,");
            tSqlBuilder.AppendLine($"\tCONSTRAINT {UnitOfWork.GetTSQLField($"PK_{tableName}")} PRIMARY KEY CLUSTERED({UnitOfWork.GetTSQLField(nameof(IDomain.ID))} ASC)");
            tSqlBuilder.AppendLine($"\tWITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON {UnitOfWork.GetTSQLField("PRIMARY")}");
            tSqlBuilder.AppendLine($") ON {UnitOfWork.GetTSQLField("PRIMARY")}");
            string result = tSqlBuilder.ToString();
            return result;
        }
        protected override string GetDeleteTableFieldTSQL(FlowTemplate flowTemplate, DataModelField dataModelField)
        {
            string tableName = GetTableName(flowTemplate.ID);
            StringBuilder tSqlBuilder = new();
            tSqlBuilder.AppendLine($"ALTER TABLE {UnitOfWork.GetTSQLField(tableName)} DROP COLUMN {UnitOfWork.GetTSQLField(dataModelField.Name)}");
            string result = tSqlBuilder.ToString();
            return result;
        }
        protected override string GetEditTableFieldTSQL(FlowTemplate flowTemplate, DataModelField oldDataModelField, DataModelField newDataModelField, IDbCommand sqliteCommand)
        {
            string tableName = GetTableName(flowTemplate.ID);
            StringBuilder tSqlBuilder = new();
            tSqlBuilder.AppendLine($"EXEC sp_rename '{tableName}.{oldDataModelField.Name}', '{newDataModelField.Name}', 'COLUMN'");
            string type = GetDataTypeByDataTypeEnum(newDataModelField.DataType);
            tSqlBuilder.AppendLine($"ALTER TABLE {UnitOfWork.GetTSQLField(tableName)} ALTER COLUMN {UnitOfWork.GetTSQLField(newDataModelField.Name)} {type} NULL");
            string result = tSqlBuilder.ToString();
            return result;
        }
        protected override string GetTableExistsTSQL(Guid flowTemplateID)
        {
            string tableName = GetTableName(flowTemplateID);
            return SqlServerRepositoryHelper.GetTableExistsTSQL(tableName);
        }
        /// <summary>
        /// 根据数据类型枚举获取数据类型
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private string GetDataTypeByDataTypeEnum(DataTypeEnum dataType)
        {
            string type = dataType switch
            {
                DataTypeEnum.Number => UnitOfWork.GetTSQLField("decimal") + "(18, 0)",
                DataTypeEnum.Date => UnitOfWork.GetTSQLField("date"),
                DataTypeEnum.Time => UnitOfWork.GetTSQLField("time"),
                DataTypeEnum.DateTime => UnitOfWork.GetTSQLField("datetime2"),
                DataTypeEnum.Boole => UnitOfWork.GetTSQLField("bit"),
                _ => UnitOfWork.GetTSQLField("varchar") + "(MAX)"
            };
            return type;
        }
    }
}
