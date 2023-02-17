using Materal.ConvertHelper;
using Materal.Gateway.OcelotExtension.ConfigModel;
using System.Text;

namespace Materal.Gateway.OcelotExtension.Services
{
    public class OcelotConfigService
    {
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly FileInfo _ocelotFileInfo;
        /// <summary>
        /// Oceelot配置
        /// </summary>
        public OcelotConfigModel OcelotConfig { get; }

        public OcelotConfigService()
        {
            _ocelotFileInfo = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ocelot.json"));
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

        public async Task SaveAsync()
        {
            await OcelotConfig.SaveAsAsync(_ocelotFileInfo.FullName, _encoding);
        }
        public void Save()
        {
            OcelotConfig.SaveAs(_ocelotFileInfo.FullName, _encoding);
        }
    }
}
