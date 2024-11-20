using Materal.Gateway.ConfigModel;

namespace Materal.Gateway.Repositories
{
    /// <summary>
    /// Ocelot配置服务
    /// </summary>
    public class OcelotConfigRepositoryImpl : IOcelotConfigRepository
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
        public OcelotConfigRepositoryImpl()
        {
            _ocelotFileInfo = new(Path.Combine(GetType().Assembly.GetDirectoryPath(), "Ocelot.json"));
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
        /// <summary>
        /// 路由排序
        /// </summary>
        public void RoutesSort()
        {
            OcelotConfig.Routes = [.. OcelotConfig.Routes.OrderBy(m => m.Index)];
            SetRoutesIndex();
        }
        /// <summary>
        /// 设置路由索引
        /// </summary>
        public void SetRoutesIndex()
        {
            for (int i = 0; i < OcelotConfig.Routes.Count; i++)
            {
                OcelotConfig.Routes[i].Index = i;
            }
        }
    }
}
