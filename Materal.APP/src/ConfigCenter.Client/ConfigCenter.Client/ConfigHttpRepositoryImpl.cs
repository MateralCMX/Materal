using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;
using ConfigCenter.Environment.PresentationModel.ConfigurationItem;
using Materal.Common;
using Materal.Model;
using System.Linq;
using Materal.StringHelper;

namespace ConfigCenter.Client
{
    public class ConfigHttpRepositoryImpl : BaseHttpClient, IConfigRepository
    {
        public async Task<List<ConfigurationItemListDTO>> GetAllConfigurationItemAsync(string address, string projectName, string[] namespaceNames)
        {
            try
            {
                var data = new QueryConfigurationItemFilterRequestModel
                {
                    ProjectName = projectName,
                    NamespaceNames = namespaceNames
                };
                var resultModel = await SendPostAsync<ResultModel<List<ConfigurationItemListDTO>>>(address, data);
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
            const string configUrl = "/ConfigurationItem/GetConfigurationItemList";
            Uri uri = new Uri(url);
            if (uri.Segments.Length == 1)
            {
                url += "/api";
            }
            url += configUrl;
            return url;
        }
    }
}
