using Materal.BusinessFlow.Abstractions.AutoNodes;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;
using System.Reflection;

namespace Materal.BusinessFlow.Services
{
    public class NodeServiceImpl : BaseServiceImpl<Node, Node, INodeRepository, QueryNodeModel>, INodeService
    {
        public NodeServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public List<AutoNodeDTO> GetAllAutoNodeList()
        {
            List<AutoNodeDTO> result = GetAllAutoNodeList(GetType().Assembly);
            return result;
        }
        public List<AutoNodeDTO> GetAllAutoNodeList(Assembly assembly)
        {
            List<AutoNodeDTO> result = assembly.GetTypes().Where(m => !m.IsAbstract && m.IsAssignableTo<IAutoNode>()).Select(m => new AutoNodeDTO(m)).ToList();
            return result;
        }
    }
}
