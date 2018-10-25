using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Materal.WPFCommon;

namespace Materal.WPFUI.CtrlTest
{
    /// <summary>
    /// WebBrowserTestCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class WebBrowserTestCtrl : UserControl
    {
        public WebBrowserTestCtrl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainWebBrowser.Source = new Uri(ViewModel.HomeAddress, UriKind.RelativeOrAbsolute);
        }

        private void BrowseBackCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = MainWebBrowser.CanGoBack;
        }

        private void BrowseBackCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MainWebBrowser.CanGoBack)
            {
                MainWebBrowser.GoBack();
            }
        }

        private void BrowseForwardCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = MainWebBrowser.CanGoForward;
        }

        private void BrowseForwardCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MainWebBrowser.CanGoForward)
            {
                MainWebBrowser.GoForward();
            }
        }

        private void BrowseHomeCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.IsLoad;
        }

        private void BrowseHomeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ViewModel.IsLoad)
            {
                MainWebBrowser.Navigate(new Uri(ViewModel.HomeAddress));
            }
        }

        private void BrowseStopCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !ViewModel.IsLoad;
        }

        private void BrowseStopCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void RefreshCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.IsLoad;
        }

        private void RefreshCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainWebBrowser.Refresh();
        }

        private void CleanUpCacheCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.IsLoad;
        }

        private void CleanUpCacheCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WebBrowserHelper.ClearCache();
            MessageBox.Show("清理缓存成功");
        }

        private void GotoPageCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void GotoPageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void MainWebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            ViewModel.IsLoad = true;
        }
    }
}
