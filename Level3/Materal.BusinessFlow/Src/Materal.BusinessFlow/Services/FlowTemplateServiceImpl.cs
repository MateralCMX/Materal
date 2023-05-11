using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;

namespace Materal.BusinessFlow.Services
{
    public class FlowTemplateServiceImpl : BaseServiceImpl<FlowTemplate, FlowTemplate, IFlowTemplateRepository, QueryFlowTemplateModel>, IFlowTemplateService
    {
        public FlowTemplateServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public override async Task EditAsync(FlowTemplate model)
        {
            model.Validation();
            FlowTemplate domain = await DefaultRepository.FirstAsync(model.ID);
            if (domain.DataModelID != model.DataModelID) throw new BusinessFlowException("不能修改绑定的数据模型");
            model.CopyProperties(domain);
            domain.Validation();
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
        }
    }
}
