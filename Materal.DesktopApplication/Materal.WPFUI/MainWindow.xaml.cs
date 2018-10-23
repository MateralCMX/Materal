using Materal.WPFUI.CtrlTest;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            LoadNumberBoxTestCtrlCommand_Executed(null, null);
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
        private void LoadNumberBoxTestCtrlCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new NumberBoxTestCtrl());
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
    }
}
