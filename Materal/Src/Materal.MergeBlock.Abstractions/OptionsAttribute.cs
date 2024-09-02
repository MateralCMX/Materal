namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 配置项特性，用于声明配置节点名称等
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class OptionsAttribute(string section) : Attribute
    {
        /// <summary>
        /// 配置文件中对应的节点名称
        /// </summary>
        public string SectionName { get; private set; } = section;
    }
}
