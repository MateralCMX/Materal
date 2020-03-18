using Materal.ConfigCenter.ProtalServer.Domain.Repositories;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.ProtalServer.HttpRepository
{
    public class ConfigServerRepositoryImpl : HttpRepositoryImpl, IConfigServerRepository
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task HealthAsync(string address)
        {
            await SendGetAsync($"{address}/api/Health/Health");
        }
    }
}
