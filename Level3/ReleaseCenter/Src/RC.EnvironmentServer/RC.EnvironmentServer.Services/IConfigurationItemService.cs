using Materal.Utils.Model;
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
        [DataValidation]
        Task SyncConfigAsync(SyncConfigModel model);
    }
}
