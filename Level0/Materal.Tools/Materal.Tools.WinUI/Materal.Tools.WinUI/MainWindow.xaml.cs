using Materal.Tools.WinUI.ViewModels;
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
        public MessageDialogViewModel MessageDialogViewModel { get; } = new();
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
            IEnumerable<Type> pageTypes = GetType().Assembly.GetTypes().Where(m => typeof(Page).IsAssignableFrom(m));
            List<MenuViewModel> menus = [];
            foreach (Type pageType in pageTypes)
            {
                MenuAttribute? menuAttribute = pageType.GetCustomAttribute<MenuAttribute>();
                if (menuAttribute is null) return;
                menus.Add(new(pageType, menuAttribute.Name, menuAttribute.Icon, menuAttribute.Index));
            }
            menus = [.. menus.OrderBy(m => m.Index).ThenBy(m => m.Name)];
            foreach (MenuViewModel menu in menus)
            {
                NavigationViewItem item = new()
                {
                    Content = menu.Name,
                    Tag = menu.PageType,
                    Icon = new FontIcon() { Glyph = menu.Icon }
                };
                MainNavigationView.MenuItems.Add(item);
            }
        }
        public async void ShowMessage(Exception ex)
        {
            MessageDialogViewModel.ChangeMessage(ex);
            await MyContentDialog.ShowAsync();
        }
    }
}
