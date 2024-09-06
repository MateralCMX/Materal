using Materal.Extensions;
using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Web.Abstractions.ControllerHttpHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RC.Core.Abstractions;

namespace RC.Core.Application
{
    /// <summary>
    /// RC模块
    /// </summary>
    public abstract class RCModule(string moduleName) : MergeBlockModule(moduleName)
    {
        /// <inheritdoc/>
        public override void OnPreConfigureServices(ServiceConfigurationContext context)
        {
            if (context.Configuration is not IConfigurationBuilder configurationBuilder) return;
            Type moduleType = GetType();
            string configFilePath = moduleType.Assembly.GetDirectoryPath();
            configFilePath = Path.Combine(configFilePath, $"{moduleType.Namespace}.json");
            configurationBuilder.AddJsonFile(configFilePath, optional: true, reloadOnChange: true);
        }
        /// <inheritdoc/>
        public override void OnConfigureServices(ServiceConfigurationContext context)
            => context.Services.TryAddSingleton<IControllerHttpHelper, RCControllerHttpHelper>();
    }
}