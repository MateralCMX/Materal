using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Materal.Abstractions;
using Materal.Logger;
using Materal.Tools.Core.ChangeEncoding;
using Materal.Tools.WinUI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MateralTools.ViewModels
{
    public partial class ChangeEncodingViewModel : ObservableObject
    {
        /// <summary>
        /// 路径
        /// </summary>
        [ObservableProperty]
        private string _path = "E:\\Project\\古典部\\新明解\\XMJ\\XMJ.Financial\\XMJ.Financial.Application\\Services";
        /// <summary>
        /// 编码
        /// </summary>
        [ObservableProperty]
        private string _encodingName = "UTF-8";
        /// <summary>
        /// 文件名过滤器
        /// </summary>
        [ObservableProperty]
        private string _fileNameFilter = "^.+\\.cs$";
        /// <summary>
        /// 强制读取
        /// </summary>
        [ObservableProperty]
        private bool _forcedRead = true;
        /// <summary>
        /// 读取编码
        /// </summary>
        [ObservableProperty]
        private string _readEncodingName = "GBK";
        /// <summary>
        /// 递归
        /// </summary>
        [ObservableProperty]
        private bool _recursive = true;
        /// <summary>
        /// 日志消息
        /// </summary>
        public ObservableCollection<string> LogMessages { get; } = [];
        private readonly ILoggerListener _loggerListener;
        private readonly IChangeEncodingService _changeEncodingService;
        public ChangeEncodingViewModel()
        {
            _loggerListener = App.ServiceProvider.GetRequiredService<ILoggerListener>();
            _loggerListener.Subscribe(OnLog);
            _changeEncodingService = App.ServiceProvider.GetRequiredService<IChangeEncodingService>();
        }
        private void OnLog(Log log)
        {
            string message = $"{DateTime.Now}|{log.Level}|{log.Message}";
            if (log.Exception is not null)
            {
                message += log.Exception.GetErrorMessage();
            }
            LogMessages.Add(message);
        }
        [RelayCommand]
        private async Task ChangeEncodingAsync()
        {
            LogMessages.Clear();
            ChangeEncodingOptions options = new()
            {
                Filter = fileInfo => new Regex(FileNameFilter).Match(fileInfo.Name).Success,
                WriteEncoding = Encoding.GetEncoding(EncodingName),
                Recursive = Recursive
            };
            if (ForcedRead)
            {
                options.ReadEncoding = Encoding.GetEncoding(ReadEncodingName);
            }
            await _changeEncodingService.ChangeEncodingAsync(Path, options);
        }
    }
}
