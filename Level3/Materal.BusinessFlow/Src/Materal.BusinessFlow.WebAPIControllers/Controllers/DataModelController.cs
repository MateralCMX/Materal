using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models.DataModel;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class DataModelController : BusinessFlowServiceControllerBase<DataModel, DataModel, IDataModelService, AddDataModelModel, EditDataModelModel, QueryDataModelModel>
    {
        public DataModelController(IServiceProvider service) : base(service)
        {
        }
    }
}