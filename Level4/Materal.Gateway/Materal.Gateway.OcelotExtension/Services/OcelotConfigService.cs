using Materal.Gateway.Common;
using Materal.Gateway.OcelotExtension.ConfigModel;
using System.Text;

namespace Materal.Gateway.OcelotExtension.Services
{
    /// <summary>
    /// Ocelot配置服务
    /// </summary>
    public class OcelotConfigService : IOcelotConfigService
    {
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly FileInfo _ocelotFileInfo;
        /// <summary>
        /// Ocelot配置
        /// </summary>
        public OcelotConfigModel OcelotConfig { get; private set; } = new();
        /// <summary>
        /// 构造方法
        /// </summary>
        public OcelotConfigService()
        {
            _ocelotFileInfo = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ocelot.json"));
            Reload();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync() => await SaveAsync(OcelotConfig);
        /// <summary>
        /// 保存
        /// </summary>
        public void Save() => Save(OcelotConfig);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="ocelotConfig"></param>
        /// <returns></returns>
        public async Task SaveAsync(OcelotConfigModel ocelotConfig) => await ocelotConfig.SaveAsAsync(_ocelotFileInfo.FullName, _encoding);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="ocelotConfig"></param>
        public void Save(OcelotConfigModel ocelotConfig) => ocelotConfig.SaveAs(_ocelotFileInfo.FullName, _encoding);
        /// <summary>
        /// 重新加载
        /// </summary>
        /// <exception cref="GatewayException"></exception>
        public void Reload()
        {
            if (!_ocelotFileInfo.Exists)
            {
                OcelotConfig = new();
                OcelotConfig.SaveAsAsync(_ocelotFileInfo.FullName, _encoding).Wait();
                _ocelotFileInfo.Refresh();
            }
            else
            {
                string configContent = File.ReadAllText(_ocelotFileInfo.FullName);
                OcelotConfig = configContent.JsonToObject<OcelotConfigModel>() ?? throw new GatewayException("反序列化失败");
            }
        }
        /// <summary>
        /// 重新加载
        /// </summary>
        /// <returns></returns>
        public Task ReloadAsync()
        {
            Reload();
            return Task.CompletedTask;
        }
    }
}
