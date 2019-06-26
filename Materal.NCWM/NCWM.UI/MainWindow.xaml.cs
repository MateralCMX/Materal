﻿using System.Collections.Generic;
using System.Diagnostics;
using NCWM.Model;
using NCWM.UI.Windows.ConfigSetting;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NCWM.UI.Ctrls.Server;

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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            KillProgram();
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
            StartServer();
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopServerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StopServer();
        }

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
            }

            if (MainTabControl.Items.Count > 0)
            {
                MainTabControl.SelectedIndex = 0;
            }
        }
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
            StopServerCommand_Executed(sender, e);
            Close();
        }
        #endregion
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
    }
}
