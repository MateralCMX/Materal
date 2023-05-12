using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services.Models.Step;

namespace Materal.BusinessFlow.Abstractions.Services
{
    public interface IStepService : IBaseService<Step, Step, IStepRepository, AddStepModel, EditStepModel, QueryStepModel>
    {
        /// <summary>
        /// 根据流程模版唯一标识获取列表信息
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <returns></returns>
        Task<List<Step>> GetListByFlowTemplateIDAsync(Guid flowTemplateID);
    }
}
