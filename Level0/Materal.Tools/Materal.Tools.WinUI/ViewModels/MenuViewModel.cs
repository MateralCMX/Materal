using System;

namespace Materal.Tools.WinUI.ViewModels
{
    public class MenuViewModel(Type pageType, string name, string icon, int index)
    {
        public Type PageType { get; } = pageType;
        public string Name { get; } = name;
        public string Icon { get; } = icon;
        public int Index { get; } = index;
    }
}
