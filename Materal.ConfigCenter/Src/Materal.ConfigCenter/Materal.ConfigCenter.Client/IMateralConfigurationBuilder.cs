using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Materal.ConfigCenter.Client
{
    public interface IMateralConfigurationBuilder : IConfigurationBuilder
    {
        /// <summary>
        /// 添加命名空间
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns></returns>
        IMateralConfigurationBuilder AddNamespace(string @namespace);
        /// <summary>
        /// 添加默认命名空间
        /// </summary>
        /// <returns></returns>
        IMateralConfigurationBuilder AddDefaultNamespace();
        /// <summary>
        /// 生成配置
        /// </summary>
        /// <returns></returns>
        Task<IConfigurationRoot> BuildMateralConfigAsync();
    }
}
