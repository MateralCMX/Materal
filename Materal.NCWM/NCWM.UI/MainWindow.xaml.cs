using NCWM.UI.Ctrls.ConfigSetting;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NCWM.UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region 事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigSettingCommand_Executed(null, null);
        }
        /// <summary>
        /// 配置管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigSettingCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoadUserControl(new ConfigSettingCtrl());
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
