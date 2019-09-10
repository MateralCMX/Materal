using System;
using NCWM.Model;
using NCWM.UI.Ctrls.Server;
using NCWM.UI.Windows.About;
using NCWM.UI.Windows.ConfigSetting;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NCWM.UI
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
        #region 事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            KillProgram();
            if (ApplicationConfig.AutoStart)
            {
                StartServer();
            }
        }
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            StopServer();
            if (MainTabControl.Items.Count > 0)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 配置管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigSettingCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var window = new ConfigSettingWindow();
            window.ShowDialog();
        }
        /// <summary>
        /// 是否可以开始服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartServerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !ViewModel.IsRun;
        }
        /// <summary>
        /// 是否可以重启或者停止服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReStartOrStopServerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.IsRun;
        }
        /// <summary>
        /// 开始服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartServerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                StartServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopServerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                StopServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 重启服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReStartServerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StopServer();
            StartServer();
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var window = new AboutWindow();
            window.ShowDialog();
        }
        #endregion
        #region 私有方法
        /// <summary>
        /// 杀死程序
        /// </summary>
        private void KillProgram()
        {
            Process[] processes = Process.GetProcessesByName("dotnet");
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process in processes)
            {
                if (currentProcess.Id == process.Id) continue;
                var isKill = false;
                foreach (ProcessModule processModule in process.Modules)
                {
                    foreach (ConfigModel config in ApplicationConfig.Configs)
                    {
                        if (processModule.ModuleName != config.MainModuleName) continue;
                        process.Kill();
                        isKill = true;
                        break;
                    }
                    if (isKill)
                    {
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 开启服务
        /// </summary>
        private void StartServer()
        {
            ViewModel.IsRun = true;
            foreach (ConfigModel config in ApplicationConfig.Configs)
            {
                var serverCtrl = new ServerCtrl(config);
                MainTabControl.Items.Add(new TabItem
                {
                    Header = config.Name,
                    Content = serverCtrl
                });
                serverCtrl.StartServer();
            }

            if (MainTabControl.Items.Count > 0)
            {
                MainTabControl.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 关闭服务
        /// </summary>
        private void StopServer()
        {
            ViewModel.IsRun = false;
            for (var i = 0; i < MainTabControl.Items.Count; i++)
            {
                object item = MainTabControl.Items[i];
                if (!(item is TabItem tabItem) || !(tabItem.Content is ServerCtrl serverCtrl)) continue;
                serverCtrl.StopServer();
                MainTabControl.Items.RemoveAt(i--);
            }
            if (MainTabControl.Items.Count > 0)
            {
                MessageBox.Show("服务器停止失败");
            }
        }
        #endregion
    }
}
