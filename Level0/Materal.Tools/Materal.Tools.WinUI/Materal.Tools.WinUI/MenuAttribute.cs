using System;

namespace Materal.Tools.WinUI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MenuAttribute(string name, string icon = "\uE78A") : Attribute
    {
        public string Name { get; } = name;
        public string Icon { get; } = icon;
    }
}
