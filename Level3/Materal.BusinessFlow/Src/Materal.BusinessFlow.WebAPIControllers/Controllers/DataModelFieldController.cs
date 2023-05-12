using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models.DataModelField;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class DataModelFieldController : BusinessFlowServiceControllerBase<DataModelField, DataModelFieldDTO, IDataModelFieldService, AddDataModelFieldModel, EditDataModelFieldModel, QueryDataModelFieldModel>
    {
        public DataModelFieldController(IServiceProvider service) : base(service)
        {
        }
    }
}