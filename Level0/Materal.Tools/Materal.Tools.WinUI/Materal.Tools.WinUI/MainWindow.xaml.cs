using Materal.Extensions;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Materal.Tools.WinUI
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadMenu();
        }
        /// <summary>
        /// 选中菜单项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MainNavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is not NavigationViewItem selectedItem || selectedItem.Tag is null || selectedItem.Tag is not Type targetType) return;
            contentFrame.Navigate(targetType);
        }
        /// <summary>
        /// 加载菜单
        /// </summary>
        private void LoadMenu()
        {
            IEnumerable<Type> pageTypes = GetType().Assembly.GetTypes().Where(m => m.IsAssignableTo<Page>());
            foreach (Type pageType in pageTypes)
            {
                MenuAttribute? menuAttribute = pageType.GetCustomAttribute<MenuAttribute>();
                if (menuAttribute is null) return;
                NavigationViewItem item = new()
                {
                    Content = menuAttribute.Name,
                    Tag = pageType,
                    Icon = new FontIcon() { Glyph = menuAttribute.Icon }
                };
                MainNavigationView.MenuItems.Add(item);
            }
        }
    }
}
