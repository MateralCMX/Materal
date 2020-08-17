using System;
using Materal.Common;
using Materal.ConDep.Center.PresentationModel.Server;
using Materal.ConDep.Common;
using Materal.ConDep.Services;
using Materal.Model;
using Materal.NetworkHelper;
using System.Threading.Tasks;

namespace Materal.ConDep.ServiceImpl
{
    public class ServerManageImpl : IServerManage
    {
        public async Task RegisterServerAsync()
        {
            try
            {
                var requestModel = new RegisterServerRequestModel
                {
                    Name = ApplicationConfig.ConDepName,
                    Address = $"{ApplicationConfig.ServerConfig.Host}:{ApplicationConfig.ServerConfig.Port}"
                };
                string url = $"http://{ApplicationConfig.CenterServerConfig.Host}:{ApplicationConfig.CenterServerConfig.Port}";
                ConsoleHelper.ServerWriteLine($"开始向{url}注册服务");
                url = $"{url}/api/Server/RegisterServer";
                var result = await HttpManager.SendPostAsync<ResultModel>(url, requestModel);
                if (result.ResultType != ResultTypeEnum.Success) throw new MateralConDepException("注册服务失败");
                ConsoleHelper.ServerWriteLine("服务注册成功");
            }
            catch (Exception exception)
            {
                throw new MateralConDepException("注册服务失败", exception);
            }
        }
    }
}
