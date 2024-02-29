using Materal.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Materal.WebAPITest
{
    public class MyControllerActivatorProvider(IControllerActivator controllerActivator) : IControllerActivatorProvider
    {
        private readonly ControllerActivatorProvider _controllerActivatorProvider = new(controllerActivator);

        public Func<ControllerContext, object> CreateActivator(ControllerActionDescriptor descriptor)
        {
            Func<ControllerContext, object> activator = _controllerActivatorProvider.CreateActivator(descriptor);
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
        public Action<ControllerContext, object>? CreateReleaser(ControllerActionDescriptor descriptor) => _controllerActivatorProvider.CreateReleaser(descriptor);
    }
}