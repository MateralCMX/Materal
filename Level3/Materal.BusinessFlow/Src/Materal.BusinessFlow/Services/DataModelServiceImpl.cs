using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models.DataModel;

namespace Materal.BusinessFlow.Services
{
    public class DataModelServiceImpl : BaseServiceImpl<DataModel, DataModel, IDataModelRepository, AddDataModelModel, EditDataModelModel, QueryDataModelModel>, IDataModelService
    {
        private readonly IDataModelFieldRepository _dataModelFieldRepository;
        public DataModelServiceImpl(IServiceProvider serviceProvider, IDataModelFieldRepository dataModelFieldRepository) : base(serviceProvider)
        {
            _dataModelFieldRepository = dataModelFieldRepository;
        }
        public override async Task DeleteAsync(Guid id)
        {
            List<DataModelField> domains = await _dataModelFieldRepository.FindAsync(m => m.DataModelID == id);
            foreach (DataModelField domain in domains)
            {
                UnitOfWork.RegisterDelete(domain);
            }
            await base.DeleteAsync(id);
        }
    }
}
