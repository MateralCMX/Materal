#nullable enable
using RC.Core.HttpClient;
using Materal.Utils.Model;
using AutoMapper;
using RC.ServerCenter.DataTransmitModel.Server;

namespace RC.ServerCenter.HttpClient
{
    public partial class ServerHttpClient : HttpClientBase
    {
        public ServerHttpClient(IServiceProvider serviceProvider) : base("RC.ServerCenter", serviceProvider) { }
        /// <summary>
        /// 获得发布程序列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<DeployListDTO>?> GetDeployListAsync() => await GetResultModelByGetAsync<List<DeployListDTO>>("Server/GetDeployList");
        /// <summary>
        /// 获得环境服务程序列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnvironmentServerListDTO>?> GetEnvironmentServerListAsync() => await GetResultModelByGetAsync<List<EnvironmentServerListDTO>>("Server/GetEnvironmentServerList");
    }
}
