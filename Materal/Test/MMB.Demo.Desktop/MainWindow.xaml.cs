using Materal.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
namespace MMB.Demo.Desktop
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //LoadMenu();
        }

        private void Test_Loaded(object sender, RoutedEventArgs e)
        {
            ILogger<MainWindow> logger = MateralServices.ServiceProvider.GetRequiredService<ILogger<MainWindow>>();
            Test.Text = logger.GetType().FullName;
        }
        ///// <summary>
        ///// 导航视图选择更改事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="args"></param>
        //private void MainNavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        //{
        //    if (args.SelectedItem is not NavigationViewItem selectedItem || selectedItem.Tag is null || selectedItem.Tag is not Type targetType) return;
        //    contentFrame.Navigate(targetType);
        //}
        ///// <summary>
        ///// 加载菜单
        ///// </summary>
        //private void LoadMenu()
        //{
        //    IEnumerable<Type> pageTypes = GetType().Assembly.GetTypes().Where(m => m.IsAssignableTo<Page>());
        //    foreach (Type pageType in pageTypes)
        //    {
        //        MenuAttribute? menuAttribute = pageType.GetCustomAttribute<MenuAttribute>();
        //        if (menuAttribute is null) return;
        //        NavigationViewItem item = new()
        //        {
        //            Content = menuAttribute.Name,
        //            Tag = pageType,
        //            Icon = new FontIcon() { Glyph = menuAttribute.Icon }
        //        };
        //        MainNavigationView.MenuItems.Add(item);
        //    }
        //}
    }
}
