using Materal.ConvertHelper;
using Materal.Gateway.OcelotExtension.ConfigModel;
using System.Text;

namespace Materal.Gateway.OcelotExtension.Services
{
    public class OcelotConfigService : IOcelotConfigService
    {
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly FileInfo _ocelotFileInfo;
        public OcelotConfigModel OcelotConfig { get; private set; } = new();
        public OcelotConfigService()
        {
            _ocelotFileInfo = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ocelot.json"));
            Reload();
        }
        public async Task SaveAsync() => await SaveAsync(OcelotConfig);
        public void Save() => Save(OcelotConfig);
        public async Task SaveAsync(OcelotConfigModel ocelotConfig) => await ocelotConfig.SaveAsAsync(_ocelotFileInfo.FullName, _encoding);
        public void Save(OcelotConfigModel ocelotConfig) => ocelotConfig.SaveAs(_ocelotFileInfo.FullName, _encoding);

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
                OcelotConfig = File.ReadAllText(_ocelotFileInfo.FullName).JsonToObject<OcelotConfigModel>();
            }
        }

        public Task ReloadAsync()
        {
            Reload();
            return Task.CompletedTask;
        }
    }
}
