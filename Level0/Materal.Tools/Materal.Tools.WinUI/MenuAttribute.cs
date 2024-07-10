using System;

namespace Materal.Tools.WinUI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MenuAttribute(string name, string icon = "\uE78A", int index = int.MaxValue) : Attribute
    {
        public string Name { get; } = name;
        public string Icon { get; } = icon;
        public int Index { get; } = index;
    }
}
