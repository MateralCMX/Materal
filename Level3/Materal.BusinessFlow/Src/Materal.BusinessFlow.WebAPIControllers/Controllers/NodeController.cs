using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.BusinessFlow.WebAPIControllers.Models.Node;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class NodeController : BusinessFlowServiceControllerBase<Node, INodeService, QueryNodeModel, AddNodeModel, EditNodeModel>
    {
        public NodeController(IServiceProvider service) : base(service)
        {
        }
    }
}