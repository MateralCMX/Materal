using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;
using ConfigCenter.Environment.PresentationModel.ConfigurationItem;
using Materal.Common;
using Materal.Model;

namespace ConfigCenter.Client
{
    public class ConfigHttpRepositoryImpl : BaseHttpClient, IConfigRepository
    {
        public async Task<List<ConfigurationItemListDTO>> GetConfigurationItemsAsync(string address, string projectName, params string[] namespaceNames)
        {
            try
            {
                QueryConfigurationItemFilterRequestModel data = new()
                {
                    ProjectName = projectName,
                    NamespaceNames = namespaceNames
                };
                ResultModel<List<ConfigurationItemListDTO>> resultModel = await SendPostAsync<ResultModel<List<ConfigurationItemListDTO>>>(address, data);
                if (resultModel.ResultType == ResultTypeEnum.Success)
                {
                    return resultModel.Data;
                }
                throw new MateralConfigCenterClientException(resultModel.Message);
            }
            catch (Exception exception)
            {
                throw new MateralConfigCenterClientException("获取配置失败", exception);
            }
        }

        protected override string GetUrl(string url)
        {
            return url.EndsWith("/") ?
                $"{url}api/ConfigurationItem/GetConfigurationItemList" :
                $"{url}/api/ConfigurationItem/GetConfigurationItemList";
        }
    }
}
