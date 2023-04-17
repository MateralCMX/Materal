using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.ADONETRepository.Extensions;
using Materal.BusinessFlow.ADONETRepository.Repositories;
using System.Data;
using System.Text;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class FlowRepositoryImpl : BaseFlowRepositoryImpl, IFlowRepository
    {
        public FlowRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        protected override string GetAddTableFieldTSQL(FlowTemplate flowTemplate, DataModelField dataModelField)
        {
            string tableName = GetTableName(flowTemplate.ID);
            StringBuilder tSqlBuilder = new();
            string type = GetDataTypeByDataTypeEnum(dataModelField.DataType);
            tSqlBuilder.AppendLine($"ALTER TABLE {UnitOfWork.GetTSQLField(tableName)} ADD COLUMN {UnitOfWork.GetTSQLField(dataModelField.Name)} {type} NULL");
            string result = tSqlBuilder.ToString();
            return result;
        }
        protected override string GetCreateTableTSQL(FlowTemplate flowTemplate, IDbCommand sqliteCommand)
        {
            List<DataModelField> dataModelFields = GetDataModelFields(flowTemplate.DataModelID, sqliteCommand);
            return GetCreateTableTSQL(flowTemplate, dataModelFields);
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
            if (oldDataModelField.ID != newDataModelField.ID) throw new BusinessFlowException("旧字段与新字段ID不同");
            string tableName = GetTableName(flowTemplate.ID);
            string backTableName = $"{tableName}_back";
            StringBuilder tSqlBuilder = new();
            tSqlBuilder.AppendLine($"ALTER TABLE {UnitOfWork.GetTSQLField(tableName)} RENAME TO {UnitOfWork.GetTSQLField(backTableName)};");
            List<DataModelField> dataModelFields = GetDataModelFields(flowTemplate.DataModelID, sqliteCommand);
            List<DataModelField> newDataModelFields = dataModelFields.ToJson().JsonToObject<List<DataModelField>>();
            for (int i = 0; i < newDataModelFields.Count; i++)
            {
                if (newDataModelFields[i].ID != oldDataModelField.ID) continue;
                newDataModelFields.RemoveAt(i);
                newDataModelFields.Insert(i, newDataModelField);
                break;
            }
            string createTSql = GetCreateTableTSQL(flowTemplate, newDataModelFields);
            tSqlBuilder.AppendLine(createTSql);
            string copyDataTSql = GetCopyTableTSQL(backTableName, dataModelFields, tableName, newDataModelFields);
            tSqlBuilder.AppendLine(copyDataTSql);
            tSqlBuilder.AppendLine($"DROP TABLE {UnitOfWork.GetTSQLField(backTableName)};");
            string result = tSqlBuilder.ToString();
            return result;
        }
        protected override string GetTableExistsTSQL(Guid flowTemplateID)
        {
            string tableName = GetTableName(flowTemplateID);
            return SqliteRepositoryHelper.GetTableExistsTSQL(UnitOfWork, tableName);
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
                DataTypeEnum.Number => "NUMERIC",
                DataTypeEnum.Date => "DATE",
                DataTypeEnum.Time => "TIME",
                DataTypeEnum.DateTime => "DATETIME",
                DataTypeEnum.Boole => "BOOLEAN",
                _ => "TEXT"
            };
            return type;
        }
        private List<DataModelField> GetDataModelFields(Guid dataModelID, IDbCommand sqliteCommand)
        {
            List<DataModelField> result = new();
            sqliteCommand.AddParameter($"{UnitOfWork.ParamsPrefix}{nameof(DataModelField.DataModelID)}", dataModelID);
            sqliteCommand.CommandText = @$"SELECT {UnitOfWork.GetTSQLField(nameof(DataModelField.ID))},{UnitOfWork.GetTSQLField(nameof(DataModelField.Name))},{UnitOfWork.GetTSQLField(nameof(DataModelField.DataType))}
FROM {UnitOfWork.GetTSQLField(nameof(DataModelField))}
WHERE {UnitOfWork.GetTSQLField(nameof(DataModelField.DataModelID))} = {UnitOfWork.ParamsPrefix}{nameof(DataModelField.DataModelID)}";
            using IDataReader dr = sqliteCommand.ExecuteReader();
            while (dr.Read())
            {
                string dataName = dr.GetString(0);
                DataTypeEnum dataType = (DataTypeEnum)dr.GetByte(1);
                string type = GetDataTypeByDataTypeEnum(dataType);
                DataModelField? domain = BaseRepositoryHelper.DataReaderConvertToDomain<DataModelField>(dr);
                if (domain == null) continue;
                result.Add(domain);
            }
            return result;
        }
        private string GetCreateTableTSQL(FlowTemplate flowTemplate, List<DataModelField> dataModelFields)
        {
            string tableName = GetTableName(flowTemplate.ID);
            StringBuilder tSqlBuilder = new();
            tSqlBuilder.AppendLine($"CREATE TABLE {tableName}(");
            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField(nameof(IBaseDomain.ID))} TEXT NOT NULL,");
            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField("FlowTemplateID")} TEXT NOT NULL,");
            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField("StartStepID")} TEXT NOT NULL,");
            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField("StepID")} TEXT NOT NULL,");
            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField("State")} INTEGER NOT NULL,");
            tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField("InitiatorID")} TEXT NOT NULL,");
            foreach (DataModelField item in dataModelFields)
            {
                string type = GetDataTypeByDataTypeEnum(item.DataType);
                tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField(item.Name)} {UnitOfWork.GetTSQLField(type)} NULL,");
            }
            tSqlBuilder.AppendLine($"\t{nameof(IBaseDomain.CreateTime)} DATETIME NOT NULL,");
            tSqlBuilder.AppendLine($"\tPRIMARY KEY (\"{nameof(IBaseDomain.ID)}\")");
            tSqlBuilder.AppendLine($");");
            string result = tSqlBuilder.ToString();
            return result;
        }
        private string GetCopyTableTSQL(string oldTableName, List<DataModelField> oldDataModelFields, string newTableName, List<DataModelField> newDataModelFields)
        {
            StringBuilder tSqlBuilder = new();
            List<string> insertArgs = new();
            List<string> selectArgs = new();
            foreach (DataModelField newField in newDataModelFields)
            {
                DataModelField? oldField = oldDataModelFields.FirstOrDefault(m => m.ID == newField.ID);
                if (oldField == null) continue;
                insertArgs.Add(UnitOfWork.GetTSQLField(newField.Name));
                selectArgs.Add(UnitOfWork.GetTSQLField(oldField.Name));
            }
            tSqlBuilder.AppendLine($"INSERT INTO {UnitOfWork.GetTSQLField(newTableName)} ({UnitOfWork.GetTSQLField(nameof(IBaseDomain.ID))},{UnitOfWork.GetTSQLField("FlowTemplateID")},{UnitOfWork.GetTSQLField("StartStepID")},{UnitOfWork.GetTSQLField("StepID")},{UnitOfWork.GetTSQLField("State")},{UnitOfWork.GetTSQLField("InitiatorID")},{UnitOfWork.GetTSQLField(nameof(IBaseDomain.CreateTime))},{string.Join(",", insertArgs)})");
            tSqlBuilder.AppendLine($"SELECT {UnitOfWork.GetTSQLField(nameof(IBaseDomain.ID))},{UnitOfWork.GetTSQLField("FlowTemplateID")},{UnitOfWork.GetTSQLField("StartStepID")},{UnitOfWork.GetTSQLField("StepID")},{UnitOfWork.GetTSQLField("State")},{UnitOfWork.GetTSQLField("InitiatorID")},{UnitOfWork.GetTSQLField(nameof(IBaseDomain.CreateTime))},{string.Join(",", selectArgs)} FROM {UnitOfWork.GetTSQLField(oldTableName)};");
            string result = tSqlBuilder.ToString();
            return result;
        }
    }
}
