using Materal.Gateway.OcelotExtension.ConfigModel;
using Materal.Gateway.OcelotExtension.Services;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.WebAPI.Controllers
{
    /// <summary>
    /// �������ṩ�߿�����
    /// </summary>
    public class ServiceDiscoveryProviderController(IOcelotConfigService ocelotConfigService) : GatewayControllerBase
    {
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> SetConfigAsync(ServiceDiscoveryProviderModel? model)
        {
            ocelotConfigService.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider = model;
            await ocelotConfigService.SaveAsync();
            return ResultModel.Success("���óɹ�");
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<ServiceDiscoveryProviderModel?> GetConfig()
        {
            ServiceDiscoveryProviderModel? result = ocelotConfigService.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider;
            return ResultModel<ServiceDiscoveryProviderModel?>.Success(result, "��ȡ�ɹ�");
        }
    }
}
