using Materal.BaseCore.CodeGenerator;
using RC.EnvironmentServer.Services.Models.ConfigurationItem;

namespace RC.EnvironmentServer.Services
{
    public partial interface IConfigurationItemService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        Task InitAsync();
        /// <summary>
        /// 同步配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [MapperController(MapperType.Put)]
        Task SyncConfigAsync(SyncConfigModel model);
    }
}
