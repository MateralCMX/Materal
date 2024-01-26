using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Materal.MergeBlock.DependencyInjection
{
    /// <summary>
    /// 控制器激活器提供者
    /// </summary>
    /// <param name="controllerActivator"></param>
    public class MergeBlockControllerActivatorProvider(IControllerActivator controllerActivator) : IControllerActivatorProvider
    {
        private readonly ControllerActivatorProvider _controllerActivatorProvider = new(controllerActivator);
        /// <summary>
        /// 创建激活器
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public Func<ControllerContext, object> CreateActivator(ControllerActionDescriptor descriptor)
        {
            Func<ControllerContext, object> activator = _controllerActivatorProvider.CreateActivator(descriptor);
            return activator;
        }
        /// <summary>
        /// 创建释放器
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public Action<ControllerContext, object>? CreateReleaser(ControllerActionDescriptor descriptor) => _controllerActivatorProvider.CreateReleaser(descriptor);
    }
}