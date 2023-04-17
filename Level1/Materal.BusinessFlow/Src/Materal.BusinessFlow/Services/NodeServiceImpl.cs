﻿using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;

namespace Materal.BusinessFlow.Services
{
    public class NodeServiceImpl : BaseServiceImpl<Node, INodeRepository, QueryNodeModel>, INodeService
    {
        public NodeServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
