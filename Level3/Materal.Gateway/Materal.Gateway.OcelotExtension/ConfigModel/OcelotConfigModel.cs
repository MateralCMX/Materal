using Materal.ConvertHelper;
using System.Text;

namespace Materal.Gateway.OcelotExtension.ConfigModel
{
    public class OcelotConfigModel
    {
        /// <summary>
        /// 路由配置组
        /// </summary>
        public List<RouteConfigModel> Routes { get; set; } = new();
        /// <summary>
        /// 全局配置项
        /// </summary>
        public GlobalConfigurationModel GlobalConfiguration { get; set; } = new();
        /// <summary>
        /// Swagger配置
        /// </summary>
        public List<SwaggerEndPointsModel> SwaggerEndPoints { get; set; } = new();
        /// <summary>
        /// 构造方法
        /// </summary>
        public OcelotConfigModel() { }
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<OcelotConfigModel> CreateByFileAsync(string filePath, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            string configString = await File.ReadAllTextAsync(filePath, encoding);
            OcelotConfigModel result = configString.JsonToObject<OcelotConfigModel>();
            return result;
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public async Task SaveAsAsync(string filePath, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            string jsonString = this.ToJson();
            FileInfo fileInfo = new(filePath);
            if(fileInfo.Directory != null && !fileInfo.Directory.Exists) fileInfo.Directory.Create();
            await File.WriteAllTextAsync(filePath, jsonString, encoding);
        }
    }
}
