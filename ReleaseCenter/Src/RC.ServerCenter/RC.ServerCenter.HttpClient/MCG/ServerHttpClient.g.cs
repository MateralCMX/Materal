#nullable enable
using RC.Core.HttpClient;
using RC.ServerCenter.DataTransmitModel.Server;
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.Common.Utils.TreeHelper;
using Materal.BaseCore.Common.Utils.IndexHelper;
using Materal.BaseCore.PresentationModel;
using Materal.Model;

namespace RC.ServerCenter.HttpClient
{
    public partial class ServerHttpClient : HttpClientBase
    {
        public ServerHttpClient() : base("RC.ServerCenter") { }
        /// <summary>
        /// 获得发布程序列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<DeployListDTO>?> GetDeployListAsync() => await GetResultModelByGetAsync<List<DeployListDTO>>("Server/GetDeployList", null, null);
        /// <summary>
        /// 获得环境服务程序列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<EnvironmentServerListDTO>?> GetEnvironmentServerListAsync() => await GetResultModelByGetAsync<List<EnvironmentServerListDTO>>("Server/GetEnvironmentServerList", null, null);
    }
}
