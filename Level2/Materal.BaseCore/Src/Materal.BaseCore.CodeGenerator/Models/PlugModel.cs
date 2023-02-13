namespace Materal.BaseCore.CodeGenerator.Models
{
    /// <summary>
    /// 插件模型
    /// </summary>
    public class PlugModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 执行数据组
        /// </summary>
        public List<string> ExcuteDomainNames { get; set; } = new List<string>();
    }
}
