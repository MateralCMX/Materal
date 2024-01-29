using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Materal.MergeBlock.WebModule
{
    /// <summary>
    /// 控制器激活器提供者
    /// </summary>
    /// <param name="controllerActivator"></param>
    public class MergeBlockControllerActivatorProvider(IControllerActivator controllerActivator) : IControllerActivatorProvider
    {
        private readonly ControllerActivatorProvider _controllerActivatorProvider = new(controllerActivator);
        /// <summary>
        /// 创建控制器
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public Func<ControllerContext, object> CreateActivator(ControllerActionDescriptor descriptor)
        {
            var activator = _controllerActivatorProvider.CreateActivator(descriptor);
            object result(ControllerContext context)
            {
                if (context.HttpContext.RequestServices is not MateralServiceProvider)
                {
                    context.HttpContext.RequestServices = new MateralServiceProvider(context.HttpContext.RequestServices);
                }
                object controller = activator(context);
                return controller;
            }
            return result;
        }
        /// <summary>
        /// 创建释放器
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public Action<ControllerContext, object>? CreateReleaser(ControllerActionDescriptor descriptor) => _controllerActivatorProvider.CreateReleaser(descriptor);
    }
}