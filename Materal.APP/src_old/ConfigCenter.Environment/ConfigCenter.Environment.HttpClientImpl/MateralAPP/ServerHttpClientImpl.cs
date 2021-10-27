using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.APP.DataTransmitModel;
using Materal.APP.HttpClient;
using Materal.APP.HttpManage;
using Materal.Model;

namespace ConfigCenter.Environment.HttpClientImpl.MateralAPP
{
    public class ServerHttpClientImpl : MateralAPPHttpClient, IServerManage
    {
        private const string _controllerUrl = "/api/Server/";
        public ServerHttpClientImpl(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }

        public async Task<ResultModel<List<ServerListDTO>>> GetServerListAsync()
        {
            var result = await SendGetAsync<ResultModel<List<ServerListDTO>>>($"{_controllerUrl}GetServerList");
            return result;
        }
    }
}
