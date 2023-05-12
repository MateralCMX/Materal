using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services.Models.Node;

namespace Materal.BusinessFlow.Abstractions.Services
{
    public interface INodeService : IBaseService<Node, Node, INodeRepository, AddNodeModel, EditNodeModel, QueryNodeModel>
    {
        /// <summary>
        /// 获得自动节点列表
        /// </summary>
        /// <returns></returns>
        List<AutoNodeDTO> GetAllAutoNodeList();
    }
}
