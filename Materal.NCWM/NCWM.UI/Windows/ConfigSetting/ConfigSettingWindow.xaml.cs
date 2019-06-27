using System.Windows;
using System.Windows.Input;

namespace NCWM.UI.Windows.ConfigSetting
{
    /// <summary>
    /// ConfigSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigSettingWindow
    {
        private readonly System.Windows.Forms.FolderBrowserDialog _folderBrowserDialog;
        public ConfigSettingWindow()
        {
            InitializeComponent();
            _folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = @"请选择目录"
            };
        }

        #region 事件
        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddConfigCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.AddConfig();
        }
        /// <summary>
        /// 是否可以删除配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteConfigCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.Configs?.Count > 0 && ViewModel.SelectConfig != null;
        }
        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteConfigCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.DeleteConfig();
        }
        /// <summary>
        /// 是否可以保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveConfigsCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.Configs?.Count > 0;
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveConfigsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.SaveConfig();
            MessageBox.Show("保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary>
        /// 是否可以浏览目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseCatalogCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.SelectConfig != null;
        }
        /// <summary>
        /// 浏览目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseCatalogCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewModel.SelectConfig.Path))
            {
                _folderBrowserDialog.SelectedPath = ViewModel.SelectConfig.Path;
            }
            if (_folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ViewModel.ChangeSelectConfigPath(_folderBrowserDialog.SelectedPath);
            }
        }
        #endregion
    }
}
