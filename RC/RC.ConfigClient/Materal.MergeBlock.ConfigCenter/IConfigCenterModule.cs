using Materal.MergeBlock.Abstractions;

namespace Materal.MergeBlock.ConfigCenter
{
    /// <summary>
    /// 配置中心模块
    /// </summary>
    public interface IConfigCenterModule
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        string ProjectName { get; }
        /// <summary>
        /// 命名空间
        /// </summary>
        string[] Namespaces { get; }
        /// <summary>
        /// 重载配置时间
        /// </summary>
        int ReloadSecondInterval { get; }
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        void OnPreConfigureServices(ServiceConfigurationContext context);
    }
}
