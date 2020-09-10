using System;
using System.Threading.Tasks;

namespace ConfigCenter.Hubs.Clients
{
    public interface IConfigCenterClient
    {
        /// <summary>
        /// 注册结果
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task RegisterResult(bool isSuccess, string message);

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteProject(Guid id);

        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteNamespace(Guid id);
    }
}
