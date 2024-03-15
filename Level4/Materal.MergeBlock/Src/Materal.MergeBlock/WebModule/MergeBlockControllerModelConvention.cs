using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Materal.MergeBlock.WebModule
{
    /// <summary>
    /// 控制器模型约定
    /// </summary>
    public class MergeBlockControllerModelConvention : IControllerModelConvention
    {
        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="controller"></param>
        public void Apply(ControllerModel controller)
        {
            if (controller.ApiExplorer.GroupName is not null && !string.IsNullOrWhiteSpace(controller.ApiExplorer.GroupName)) return;
            IModuleInfo? moduleInfo = MergeBlockHost.ModuleInfos.FirstOrDefault(m => m.ModuleType.IsAssignableTo<IMergeBlockWebModule>() && m.ModuleType.Assembly == controller.ControllerType.Assembly);
            if (moduleInfo == null) return;
            controller.ApiExplorer.GroupName = moduleInfo.Name;
        }
    }
}
