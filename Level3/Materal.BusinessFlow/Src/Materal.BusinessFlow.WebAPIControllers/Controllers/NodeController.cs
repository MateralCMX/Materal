using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Models;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.BusinessFlow.WebAPIControllers.Models.Node;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class NodeController : BusinessFlowServiceControllerBase<Node, INodeService, QueryNodeModel, AddNodeModel, EditNodeModel>
    {
        public NodeController(IServiceProvider service) : base(service)
        {
        }
        /// <summary>
        /// ��������Զ��ڵ��б�
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