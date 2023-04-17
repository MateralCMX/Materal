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
        protected override string GetCreateTableTSQL(FlowTemplate flowTemplate, IDbCommand sqliteCommand)
        {
            string tableName = GetTableName(flowTemplate.ID);
            StringBuilder tSqlBuilder = new();
            tSqlBuilder.AppendLine($"CREATE TABLE {tableName}(");
            tSqlBuilder.AppendLine($"\t{nameof(IBaseDomain.ID)} TEXT NOT NULL,");
            tSqlBuilder.AppendLine($"\tFlowTemplateID TEXT NOT NULL,");
            tSqlBuilder.AppendLine($"\tStartStepID TEXT NOT NULL,");
            tSqlBuilder.AppendLine($"\tStepID TEXT NOT NULL,");
            tSqlBuilder.AppendLine($"\tState INTEGER NOT NULL,");
            tSqlBuilder.AppendLine($"\tInitiatorID TEXT NOT NULL,");

            sqliteCommand.AddParameter($"{UnitOfWork.ParamsPrefix}{nameof(DataModelField.DataModelID)}", flowTemplate.DataModelID);
            sqliteCommand.CommandText = @$"SELECT {UnitOfWork.GetTSQLField(nameof(DataModelField.Name))},{UnitOfWork.GetTSQLField(nameof(DataModelField.DataType))}
FROM {UnitOfWork.GetTSQLField(nameof(DataModelField))}
WHERE {UnitOfWork.GetTSQLField(nameof(DataModelField.DataModelID))} = {UnitOfWork.ParamsPrefix}{nameof(DataModelField.DataModelID)}";
            using IDataReader dr = sqliteCommand.ExecuteReader();
            while (dr.Read())
            {
                string dataName = dr.GetString(0);
                DataTypeEnum dataType = (DataTypeEnum)dr.GetByte(1);
                string type = dataType switch
                {
                    DataTypeEnum.Number => "NUMERIC",
                    DataTypeEnum.Date => "DATE",
                    DataTypeEnum.Time => "TIME",
                    DataTypeEnum.DateTime => "DATETIME",
                    DataTypeEnum.Boole => "BOOLEAN",
                    _ => "TEXT"
                };
                tSqlBuilder.AppendLine($"\t{UnitOfWork.GetTSQLField(dataName)} {UnitOfWork.GetTSQLField(type)} NULL,");
            }

            tSqlBuilder.AppendLine($"\t{nameof(IBaseDomain.CreateTime)} DATETIME NOT NULL,");
            tSqlBuilder.AppendLine($"\tPRIMARY KEY (\"{nameof(IBaseDomain.ID)}\")");
            tSqlBuilder.AppendLine($")");
            string result = tSqlBuilder.ToString();
            return result;
        }

        protected override string GetTableExistsTSQL(Guid flowTemplateID)
        {
            string tableName = GetTableName(flowTemplateID);
            return SqliteRepositoryHelper.GetTableExistsTSQL(UnitOfWork, tableName);
        }
    }
}
