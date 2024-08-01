namespace Materal.Tools.WinUI
{
    /// <summary>
    /// 菜单特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MenuAttribute(string name, string icon = "\uE78A", int index = int.MaxValue) : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = name;
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; } = icon;
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; } = index;
    }
}
