using Materal.ConvertHelper;
using Materal.Gateway.Models;
using Materal.Gateway.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Materal.Gateway.Services
{
    /// <summary>
    /// 配置服务
    /// </summary>
    public class ConfigService
    {
        private readonly OcelotConfigModel _ocelotConfig;
        /// <summary>
        /// 配置服务
        /// </summary>
        public ConfigService()
        {
            _ocelotConfig = new OcelotConfigModel(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ocelot.json"));
        }
        /// <summary>
        /// 获得配置项组
        /// </summary>
        /// <returns></returns>
        public List<ConfigItemModel> GetConfigItems(string searchKey)
        {
            List<ConfigItemModel> result = string.IsNullOrWhiteSpace(searchKey) ? 
                _ocelotConfig.Items : 
                _ocelotConfig.Items.Where(m=>m.ServiceName.Contains(searchKey)).ToList();
            return result.OrderBy(m => m.ServiceName).ToList();
        }
        /// <summary>
        /// 获得配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ConfigItemModel GetConfigItem(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id), "唯一标识为空");
            ConfigItemModel result = _ocelotConfig.Items.FirstOrDefault(m=>m.ID == id);
            if (result == null) throw new ArgumentException("唯一标识不存在", nameof(id));
            return result;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="configItem"></param>
        public async Task AddItemAsync(ConfigItemModel configItem)
        {
            if (string.IsNullOrWhiteSpace(configItem.ServiceName)) throw new ArgumentNullException(nameof(configItem), "服务名为空");
            if (_ocelotConfig.Items.Any(m => m.ServiceName == configItem.ServiceName)) throw new ArgumentException("服务名重复", nameof(configItem));
            while (configItem.ID == Guid.Empty || _ocelotConfig.Items.Any(m=>m.ID == configItem.ID))
            {
                configItem.ID = Guid.NewGuid();
            }
            _ocelotConfig.Items.Add(configItem);
            await _ocelotConfig.SaveAsync();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="configItem">配置项</param>
        public async Task SaveItemAsync(ConfigItemModel configItem)
        {
            if (string.IsNullOrWhiteSpace(configItem.ServiceName)) throw new ArgumentNullException(nameof(configItem), "服务名为空");
            if (_ocelotConfig.Items.Any(m => m.ID != configItem.ID && m.ServiceName == configItem.ServiceName)) throw new ArgumentException("服务名重复", nameof(configItem));
            if (configItem.ID == Guid.Empty) throw new ArgumentNullException(nameof(configItem), "唯一标识为空");
            ConfigItemModel configItemFormDB = _ocelotConfig.Items.FirstOrDefault(m => m.ID == configItem.ID);
            if (configItemFormDB == null) throw new ArgumentException("唯一标识不存在", nameof(configItem));
            configItem.CopyProperties(configItemFormDB);
            await _ocelotConfig.SaveAsync();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="globalConfig">全局配置</param>
        public async Task SaveGlobalAsync(GlobalConfigModel globalConfig)
        {
            _ocelotConfig.GlobalConfig = globalConfig;
            await _ocelotConfig.SaveAsync();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public async Task DeleteItemAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id), "唯一标识为空");
            ConfigItemModel result = _ocelotConfig.Items.FirstOrDefault(m => m.ID == id);
            if (result == null) throw new ArgumentException("唯一标识不存在", nameof(id));
            _ocelotConfig.Items.Remove(result);
            await _ocelotConfig.SaveAsync();
        }
    }
}
