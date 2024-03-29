﻿using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models.DataModelField;

namespace Materal.BusinessFlow.Services
{
    public class DataModelFieldServiceImpl : BaseServiceImpl<DataModelField, DataModelFieldDTO, IDataModelFieldRepository, AddDataModelFieldModel, EditDataModelFieldModel, QueryDataModelFieldModel>, IDataModelFieldService
    {
        private readonly IFlowTemplateRepository _flowTemplateRepository;
        private readonly IFlowRepository _flowRepository;
        public DataModelFieldServiceImpl(IServiceProvider serviceProvider, IFlowTemplateRepository flowTemplateRepository, IFlowRepository flowRepository) : base(serviceProvider)
        {
            _flowTemplateRepository = flowTemplateRepository;
            _flowRepository = flowRepository;
        }
        public override async Task<Guid> AddAsync(AddDataModelFieldModel model)
        {
            model.Validation();
            DataModelField domain = model.CopyProperties<DataModelField>();
            domain.Validation();
            UnitOfWork.RegisterAdd(domain);
            List<FlowTemplate> flowTemplates = await _flowTemplateRepository.FindAsync(m => m.DataModelID == model.DataModelID);
            foreach (FlowTemplate flowTemplate in flowTemplates)
            {
                await _flowRepository.AddTableFieldAsync(flowTemplate, domain);
            }
            await UnitOfWork.CommitAsync();
            return domain.ID;
        }
        public override async Task EditAsync(EditDataModelFieldModel model)
        {
            model.Validation();
            DataModelField domain = await DefaultRepository.FirstAsync(model.ID);
            if (domain.DataType != model.DataType) throw new BusinessFlowException("不能修改数据类型");
            DataModelField oldDomain = domain.ToJson().JsonToObject<DataModelField>();
            model.CopyProperties(domain);
            domain.Validation();
            List<FlowTemplate> flowTemplates = await _flowTemplateRepository.FindAsync(m => m.DataModelID == model.DataModelID);
            foreach (FlowTemplate flowTemplate in flowTemplates)
            {
                await _flowRepository.EditTableFieldAsync(flowTemplate, oldDomain, domain);
            }
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
        }
        public override async Task DeleteAsync(Guid id)
        {
            DataModelField domain = await DefaultRepository.FirstAsync(id);
            UnitOfWork.RegisterDelete(domain);
            List<FlowTemplate> flowTemplates = await _flowTemplateRepository.FindAsync(m => m.DataModelID == domain.DataModelID);
            foreach (FlowTemplate flowTemplate in flowTemplates)
            {
                await _flowRepository.DeleteTableFieldAsync(flowTemplate, domain);
            }
            await UnitOfWork.CommitAsync();
        }
    }
}
