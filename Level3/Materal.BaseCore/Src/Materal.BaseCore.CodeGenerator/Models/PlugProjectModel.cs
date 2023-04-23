namespace Materal.BaseCore.CodeGenerator.Models
{
    public class PlugProjectModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 插件组
        /// </summary>
        public List<PlugModel> Plugs { get; set; } = new List<PlugModel>();
    }
}
