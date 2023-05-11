using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;

namespace Materal.BusinessFlow.Services
{
    public class DataModelServiceImpl : BaseServiceImpl<DataModel, DataModel, IDataModelRepository, QueryDataModelModel>, IDataModelService
    {
        public DataModelServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
