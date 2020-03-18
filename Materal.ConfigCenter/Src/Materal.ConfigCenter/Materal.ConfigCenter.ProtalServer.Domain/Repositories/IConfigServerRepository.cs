using System;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.ProtalServer.Domain.Repositories
{
    public interface IConfigServerRepository
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Task HealthAsync(string address);
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="address"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteProjectAsync(string address, Guid id);
        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="address"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteNamespaceAsync(string address, Guid id);
    }
}
