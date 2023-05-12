using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models.Node;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class NodeController : BusinessFlowServiceControllerBase<Node, Node, INodeService, AddNodeModel, EditNodeModel, QueryNodeModel>
    {
        public NodeController(IServiceProvider service) : base(service)
        {
        }
        /// <summary>
        /// 获得所有自动节点列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<AutoNodeDTO>> GetAllAutoNodeList()
        {
            List<AutoNodeDTO> result = DefaultService.GetAllAutoNodeList();
            return ResultModel<List<AutoNodeDTO>>.Success(result);
        }
    }
}