using Materal.BusinessFlow.Abstractions.AutoNodes;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.AutoNodes.Base;
using Microsoft.Extensions.Logging;

namespace Materal.BusinessFlow.AutoNodes
{
    public class ConsoleMessageAutoNode : BaseAutoNode, IAutoNode
    {
        private readonly ILogger<ConsoleMessageAutoNode>? _logger;
        public ConsoleMessageAutoNode(IServiceProvider serviceProvider, ILogger<ConsoleMessageAutoNode>? logger) : base(serviceProvider)
        {
            _logger = logger;
        }
        public override void Excute(AutoNodeModel autoNodeModel)
        {
            if (autoNodeModel.Node.Data == null || string.IsNullOrWhiteSpace(autoNodeModel.Node.Data)) return;
            DataModelField? dataModelField = autoNodeModel.DataModelFields.FirstOrDefault(m => m.Name == autoNodeModel.Node.Data);
            if(dataModelField == null || !autoNodeModel.FlowData.ContainsKey(autoNodeModel.Node.Data)) return;
            object? value = autoNodeModel.FlowData[autoNodeModel.Node.Data];
            if (value == null) return;
            string message = value.ToString();
            _logger?.LogInformation(message);
        }
    }
}
