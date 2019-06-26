using Materal.FileHelper;
using Materal.WPFCommon;
using NCWM.Manager;
using NCWM.Model;
using System;
using System.Diagnostics;
using System.IO;

namespace NCWM.UI.Ctrls.Server
{
    public class ServerViewModel : NotifyPropertyChanged
    {
        public bool IsRun => _service?.IsRun ?? false;
        private NCWMService _service;
        private ConfigModel _config;
        private string _consoleText = string.Empty;
        private bool _isSaveConsoleText = true;
        private string _consoleCommand;

        public string ConsoleText
        {
            get => _consoleText;
            private set
            {
                _consoleText = value;
                if (!string.IsNullOrEmpty(_consoleText))
                {
                    _isSaveConsoleText = false;
                }
                OnPropertyChanged();
            }
        }

        public string ConsoleCommand
        {
            get => _consoleCommand;
            set
            {
                _consoleCommand = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config"></param>
        public void Init(ConfigModel config)
        {
            _config = config;
            _service = new NCWMService(config);
            _service.OutputDataReceived += _service_OutputOrErrorDataReceived;
            _service.ErrorDataReceived += _service_OutputOrErrorDataReceived;
        }
        private void _service_OutputOrErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            ConsoleText += $"{e.Data}\r\n";
            if (_consoleText.Length >= 10 * 1024 * 1024)
            {
                ClearConsoleText();
            }
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        public void StartServer()
        {
            if (IsRun) return;
            ClearConsoleText();
            ConsoleText += $"[{DateTime.Now}]正在启动服务......\r\n";
            _service.Start();
            ConsoleText += $"[{DateTime.Now}]服务已启动\r\n";
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns></returns>
        public void StopServer()
        {
            if (!IsRun) return;
            ConsoleText += $"[{DateTime.Now}]正在停止服务......\r\n";
            _service.Stop();
            ConsoleText += $"[{DateTime.Now}]服务已停止\r\n";
            SaveConsoleText(ConsoleText);
        }
        /// <summary>
        /// 清理控制台文本
        /// </summary>
        public void ClearConsoleText()
        {
            string tempString = ConsoleText;
            ConsoleText = string.Empty;
            if (_isSaveConsoleText) return;
            SaveConsoleText(tempString);
        }
        /// <summary>
        /// 发送命令
        /// </summary>
        public void SendCommand()
        {
            _service.SendCommand(ConsoleCommand);
            ConsoleCommand = string.Empty;
        }
        /// <summary>
        /// 保存控制台文本
        /// </summary>
        /// <param name="consoleString"></param>
        private void SaveConsoleText(string consoleString)
        {
            string baseDirectoryPath = $@"{AppDomain.CurrentDomain.BaseDirectory}ConsoleHistoryText\";
            if (!Directory.Exists(baseDirectoryPath)) Directory.CreateDirectory(baseDirectoryPath);
            baseDirectoryPath += $@"{_config.Name}_{_config.TargetName}\";
            if (!Directory.Exists(baseDirectoryPath)) Directory.CreateDirectory(baseDirectoryPath);
            baseDirectoryPath += $@"{DateTime.Now:yyyyMMddHHmmssffff}.txt";
            TextFileManager.WriteText(baseDirectoryPath, consoleString);
            _isSaveConsoleText = true;
        }
    }
}
