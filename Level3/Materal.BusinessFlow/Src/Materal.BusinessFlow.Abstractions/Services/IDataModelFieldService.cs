using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services.Models.DataModelField;

namespace Materal.BusinessFlow.Abstractions.Services
{
    public interface IDataModelFieldService : IBaseService<DataModelField, DataModelFieldDTO, IDataModelFieldRepository, AddDataModelFieldModel, EditDataModelFieldModel, QueryDataModelFieldModel>
    {

    }
}
