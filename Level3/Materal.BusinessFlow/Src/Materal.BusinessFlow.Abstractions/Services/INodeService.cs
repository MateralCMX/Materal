using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Models;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services.Models;

namespace Materal.BusinessFlow.Abstractions.Services
{
    public interface INodeService : IBaseService<Node, INodeRepository, QueryNodeModel>
    {
        /// <summary>
        /// 获得自动节点列表
        /// </summary>
        /// <returns></returns>
        List<AutoNodeDTO> GetAllAutoNodeList();
    }
}
