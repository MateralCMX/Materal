#nullable enable
using RC.Core.HttpClient;
using Materal.BaseCore.PresentationModel;
using Materal.Utils.Model;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;

namespace RC.EnvironmentServer.HttpClient
{
    public partial class ConfigurationItemHttpClient : HttpClientBase<AddConfigurationItemRequestModel, EditConfigurationItemRequestModel, QueryConfigurationItemRequestModel, ConfigurationItemDTO, ConfigurationItemListDTO>
    {
        public ConfigurationItemHttpClient() : base("RC.EnvironmentServer") { }
        /// <summary>
        /// 同步配置
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task SyncConfigAsync(SyncConfigRequestModel requestModel) => await GetResultModelByPutAsync("ConfigurationItem/SyncConfig", null, requestModel);
    }
}
