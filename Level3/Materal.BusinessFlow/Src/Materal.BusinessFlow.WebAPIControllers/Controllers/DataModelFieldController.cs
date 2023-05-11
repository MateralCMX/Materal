using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.BusinessFlow.WebAPIControllers.Models.DataModelField;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class DataModelFieldController : BusinessFlowServiceControllerBase<DataModelField, DataModelFieldDTO, IDataModelFieldService, QueryDataModelFieldModel, AddDataModelFieldModel, EditDataModelFieldModel>
    {
        public DataModelFieldController(IServiceProvider service) : base(service)
        {
        }
    }
}