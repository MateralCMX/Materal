using Materal.WPFUI.CtrlTest.CornerRadiusTextBox;
using Materal.WPFUI.CtrlTest.DateTimePicker;
using Materal.WPFUI.CtrlTest.NumberBox;
using Materal.WPFUI.CtrlTest.SearchBox;
using Materal.WPFUI.CtrlTest.Test;
using Materal.WPFUI.CtrlTest.WebBrowser;
using Materal.WPFUI.Tools.NuGetTools;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Materal.WPFUI.CtrlTest.CefSharp;
using Materal.WPFUI.CtrlTest.CornerRadiusButton;
using Materal.WPFUI.CtrlTest.CornerRadiusToggleButton;
using Materal.WPFUI.CtrlTest.Edge;

namespace Materal.WPFUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载完毕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WPFUIHelper.RegisterCustomerService();
            WPFUIHelper.BuildService();
            LoadTestCtrlCommand_Executed(null, null);
        }

        #region 命令实现
        private void ReloadCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_nowControl == null) return;
            Type nowControlType = _nowControl.GetType();
            ConstructorInfo constructorInfo = nowControlType.GetConstructor(new Type[0]);
            if (constructorInfo == null) return;
            _nowControl = null;
            var userControl = (UserControl)constructorInfo.Invoke(new object[0]);
            LoadUserControl(userControl);
        }
        private void ReloadCtrlCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _nowControl != null;
        }
        private void LoadTestCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new TestCtrl());
        }
        private void LoadNumberBoxTestCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new NumberBoxTestCtrl());
        }
        private void LoadDateTimePickerTestCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new DateTimePickerTestCtrl());
        }
        private void LoadWebBrowserTestCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new WebBrowserTestCtrl());
        }
        private void LoadCefSharpTestCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new CefSharpTestCtrl());
        }

        private void LoadSearchBoxTestCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new SearchBoxTestCtrl());
        }

        private void LoadNuGetToolsCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new NuGetToolCtrl());
        }

        private void LoadCornerRadiusTextBoxCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new CornerRadiusTextBoxTestCtrl());
        }

        private void LoadCornerRadiusButtonCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new CornerRadiusButtonTestCtrl());
        }

        private void LoadCornerRadiusToggleButtonCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new CornerRadiusToggleButtonTestCtrl());
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 当前的控件
        /// </summary>
        private UserControl _nowControl;
        /// <summary>
        /// 加载用户控件
        /// </summary>
        /// <param name="userControl"></param>
        private void LoadUserControl(UserControl userControl)
        {
            if (_nowControl?.GetType().GUID == userControl.GetType().GUID) return;
            MainPanel.Children.Clear();
            userControl.VerticalAlignment = VerticalAlignment.Stretch;
            userControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            MainPanel.Children.Add(userControl);
            _nowControl = userControl;
        }
        #endregion

        private void CheckForUpdatesCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void LoadEdgeTestCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new EdgeTestCtrl());
        }
    }
}
