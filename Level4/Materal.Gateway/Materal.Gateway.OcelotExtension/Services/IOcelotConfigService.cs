﻿using Materal.Gateway.OcelotExtension.ConfigModel;

namespace Materal.Gateway.OcelotExtension.Services
{
    /// <summary>
    /// Ocelot配置服务
    /// </summary>
    public interface IOcelotConfigService
    {
        /// <summary>
        /// Oceelot配置
        /// </summary>
        public OcelotConfigModel OcelotConfig { get; }
        /// <summary>
        /// 重新加载
        /// </summary>
        void Reload();
        /// <summary>
        /// 重新加载
        /// </summary>
        /// <returns></returns>
        Task ReloadAsync();
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="ocelotConfig"></param>
        /// <returns></returns>
        Task SaveAsync(OcelotConfigModel ocelotConfig);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="ocelotConfig"></param>
        void Save(OcelotConfigModel ocelotConfig);
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
        /// <summary>
        /// 保存
        /// </summary>
        void Save();
    }
}
