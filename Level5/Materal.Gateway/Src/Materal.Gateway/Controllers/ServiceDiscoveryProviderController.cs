using Materal.Gateway.OcelotExtension.ConfigModel;
using Materal.Gateway.Service;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.Controllers
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
            await ocelotConfigService.SetServiceDiscoveryProviderAsync(model);
            return ResultModel.Success("���óɹ�");
        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<ServiceDiscoveryProviderModel?> GetConfig()
        {
            ServiceDiscoveryProviderModel? result = ocelotConfigService.GetServiceDiscoveryProviderConfig();
            return ResultModel<ServiceDiscoveryProviderModel?>.Success(result, "��ȡ�ɹ�");
        }
    }
}
