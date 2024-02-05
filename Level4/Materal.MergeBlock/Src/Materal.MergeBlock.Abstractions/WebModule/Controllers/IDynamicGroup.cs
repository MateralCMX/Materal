using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Materal.MergeBlock.Abstractions.WebModule.Controllers
{
    /// <summary>
    /// 动态分组
    /// </summary>
    public interface IDynamicGroup
    {
        /// <summary>
        /// 获取组名
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public string GetGroupName(ControllerModel controller);
    }
}
