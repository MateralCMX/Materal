using Materal.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Materal.BaseCore.WebAPI
{
    public class MateralBaseCoreControllerActivatorProvider : IControllerActivatorProvider
    {
        private ControllerActivatorProvider _controllerActivatorProvider;
        public MateralBaseCoreControllerActivatorProvider(IControllerActivator controllerActivator)
        {
            _controllerActivatorProvider = new ControllerActivatorProvider(controllerActivator);
        }
        public Func<ControllerContext, object> CreateActivator(ControllerActionDescriptor descriptor)
        {
            Func<ControllerContext, object> activator = _controllerActivatorProvider.CreateActivator(descriptor);
            object result(ControllerContext context)
            {
                context.HttpContext.RequestServices = new MateralServiceProvider(context.HttpContext.RequestServices, MateralServiceProviderFactory.DIFilter);
                object controller = activator(context);
                return controller;
            }
            return result;
        }
        public Action<ControllerContext, object>? CreateReleaser(ControllerActionDescriptor descriptor) => _controllerActivatorProvider.CreateReleaser(descriptor);
    }
}