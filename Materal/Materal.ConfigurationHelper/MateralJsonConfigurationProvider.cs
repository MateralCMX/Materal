using Materal.Common;
using Materal.ConvertHelper;
using Materal.FileHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Materal.ConfigurationHelper
{
    public class MateralJsonConfigurationProvider : JsonConfigurationProvider
    {
        public MateralJsonConfigurationProvider(FileConfigurationProvider jsonConfigurationProvider) : base((JsonConfigurationSource)jsonConfigurationProvider.Source)
        {
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            if (Source.Path == null || string.IsNullOrWhiteSpace(Source.Path)) throw new MateralException("路径为空");
            string jsonStr = Data.ToJson();
            await TextFileManager.WriteTextAsync(Source.Path, jsonStr);
        }
        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            if (Source.Path == null || string.IsNullOrWhiteSpace(Source.Path)) throw new MateralException("路径为空");
            string jsonStr = Data.ToJson();
            TextFileManager.WriteText(Source.Path, jsonStr);
        }
    }
}
