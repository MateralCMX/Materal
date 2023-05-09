using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.BusinessFlow.WebAPIControllers.Models.DataModel;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class DataModelController : BusinessFlowServiceControllerBase<DataModel, IDataModelService, QueryDataModelModel, AddDataModelModel, EditDataModelModel>
    {
        public DataModelController(IServiceProvider service) : base(service)
        {
        }
    }
}