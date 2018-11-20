using CefSharp;
using CefSharp.Wpf;
using System.Windows;
using System.Windows.Input;

namespace Materal.WPFUI.CtrlTest.CefSharp
{
    /// <summary>
    /// CefSharpTestCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class CefSharpTestCtrl
    {
        public CefSharpTestCtrl()
        {
            InitializeComponent();
        }

        private ChromiumWebBrowser _webBrowser;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _webBrowser = new ChromiumWebBrowser
            {
                Address = ViewModel.HomeAddress
            };
            MainPanel.Children.Add(_webBrowser);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _webBrowser.CloseDevTools();
            _webBrowser.GetBrowser().CloseBrowser(true);
            _webBrowser.Dispose();
        }

        private void RefreshCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _webBrowser.Reload();
        }

        private void RefreshCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_webBrowser != null)
            {
                e.CanExecute = !_webBrowser.IsLoading;
            }
        }

        private void DevToolCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _webBrowser?.ShowDevTools();
        }

        private void GotoPageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _webBrowser.Address = ViewModel.NowAddress;
        }

        private void GotoPageCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_webBrowser != null)
            {
                e.CanExecute = !_webBrowser.IsLoading;
            }
        }

        private void BrowseHomeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _webBrowser.Address = ViewModel.HomeAddress;
        }

        private void BrowseHomeCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_webBrowser != null)
            {
                e.CanExecute = !_webBrowser.IsLoading;
            }
        }

        private void BrowseStopCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _webBrowser.Stop();
        }

        private void BrowseStopCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_webBrowser != null)
            {
                e.CanExecute = _webBrowser.IsLoading;
            }
        }

        private void BrowseBackCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _webBrowser.Back();
        }

        private void BrowseBackCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_webBrowser != null)
            {
                e.CanExecute = _webBrowser.CanGoBack && !_webBrowser.IsLoading;
            }
        }

        private void BrowseForwardCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _webBrowser.Forward();
        }

        private void BrowseForwardCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_webBrowser != null)
            {
                e.CanExecute = _webBrowser.CanGoForward && !_webBrowser.IsLoading;
            }
        }
    }
}
