using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Materal.MergeBlock.DependencyInjection
{
    public class MergeBlockControllerActivatorProvider(IControllerActivator controllerActivator) : IControllerActivatorProvider
    {
        private readonly ControllerActivatorProvider _controllerActivatorProvider = new(controllerActivator);

        public Func<ControllerContext, object> CreateActivator(ControllerActionDescriptor descriptor)
        {
            Func<ControllerContext, object> activator = _controllerActivatorProvider.CreateActivator(descriptor);
            return activator;
        }
        public Action<ControllerContext, object>? CreateReleaser(ControllerActionDescriptor descriptor) => _controllerActivatorProvider.CreateReleaser(descriptor);
    }
}