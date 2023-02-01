using System;
using System.Windows;
using System.Windows.Controls;

namespace Materal.WPFUI.CtrlTest.Edge
{
    /// <summary>
    /// EdgeTestCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class EdgeTestCtrl : UserControl
    {
        public EdgeTestCtrl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //var webView = new WebView();
            //Content = webView;
            //webView.Navigate(new Uri("http://www.baidu.com"));
        }
    }
}
