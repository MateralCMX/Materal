using RC.EnvironmentServer.Abstractions.Services.Models.ConfigurationItem;

namespace RC.EnvironmentServer.Abstractions.Services
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
