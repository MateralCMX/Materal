using NCWM.Model;
using System.Windows.Controls;
using System.Windows.Input;

namespace NCWM.UI.Ctrls.Server
{
    /// <summary>
    /// ServerCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class ServerCtrl
    {
        public ServerCtrl()
        {
            InitializeComponent();
            ConfigModel config = ApplicationConfig.Configs?.Count > 0
                ? ApplicationConfig.Configs[0]
                : new ConfigModel
                {
                    DotNetCoreVersion = 2.2f,
                    Name = "日志WebAPI",
                    Path = @"E:\Project\IntegratedPlatform\Project\IntegratedPlatform\Log.WebAPI\bin\Debug\netcoreapp2.2",
                    TargetName = "Log.WebAPI",
                    Arguments = "--ConfigTarget Development --urls=http://*:8800"
                };
            ViewModel.Init(config);
        }
        public ServerCtrl(ConfigModel config)
        {
            InitializeComponent();
            ViewModel.Init(config);
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public void StopServer()
        {
            StopServerCommand_Executed(null, null);
        }

        public void StartServer()
        {
            StartServerCommand_Executed(null, null);
        }
        #region 事件

        private void StartServerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !ViewModel.IsRun;
        }

        private void ReStartServerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.IsRun;
        }

        private void StopServerCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.IsRun;
        }

        private void ClearConsoleTextCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.ConsoleText.Length > 0;
        }

        private void StartServerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.StartServer();
        }

        private void ReStartServerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StopServerCommand_Executed(null, null);
            StartServerCommand_Executed(null, null);
        }

        private void StopServerCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.StopServer();
        }

        private void ClearConsoleTextCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.ClearConsoleText();
        }
        #endregion

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.ScrollToEnd();
            }
        }

        private void SendCommandCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ViewModel.SendCommand();
        }

        private void SendCommandCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.IsRun;
        }
    }
}
