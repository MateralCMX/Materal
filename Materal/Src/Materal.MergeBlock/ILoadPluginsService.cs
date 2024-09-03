﻿namespace Materal.MergeBlock
{
    /// <summary>
    /// 加载插件服务
    /// </summary>
    public interface ILoadPluginsService
    {
        /// <summary>
        /// 获取插件
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        IEnumerable<IPlugin> GetPlugins(MergeBlockOptions config);
    }
}
