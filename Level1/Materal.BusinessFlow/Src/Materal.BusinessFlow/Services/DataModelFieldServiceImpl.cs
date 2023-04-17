using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;

namespace Materal.BusinessFlow.Services
{
    public class DataModelFieldServiceImpl : BaseServiceImpl<DataModelField, IDataModelFieldRepository, QueryDataModelFieldModel>, IDataModelFieldService
    {
        public DataModelFieldServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
