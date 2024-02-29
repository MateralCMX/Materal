using Materal.MergeBlock.Abstractions.WebModule;
using Materal.MergeBlock.Abstractions.WebModule.Controllers;
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
            #region 分组
            if (controller.Attributes.FirstOrDefault(m => m is IDynamicGroup) is IDynamicGroup dynamicGroup)
            {
                controller.ApiExplorer.GroupName = dynamicGroup.GetGroupName(controller);
            }
            else if (controller.ApiExplorer.GroupName is null || string.IsNullOrWhiteSpace(controller.ApiExplorer.GroupName))
            {
                IModuleInfo? moduleInfo = MergeBlockHost.ModuleInfos.FirstOrDefault(m => m.ModuleType.IsAssignableTo<IMergeBlockWebModule>() && m.ModuleType.Assembly == controller.ControllerType.Assembly);
                if (moduleInfo == null) return;
                controller.ApiExplorer.GroupName = moduleInfo.Name;
            }
            #endregion
        }
    }
}
