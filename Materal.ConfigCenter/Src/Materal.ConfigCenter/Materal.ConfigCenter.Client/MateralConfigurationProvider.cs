using Microsoft.Extensions.Configuration;

namespace Materal.ConfigCenter.Client
{
    public class MateralConfigurationProvider : ConfigurationProvider
    {
        /// <summary>
        /// 命名空间名称
        /// </summary>
        public string NamespaceName { get; }
        public MateralConfigurationProvider(string namespaceName)
        {
            NamespaceName = namespaceName;
        }
    }
}
