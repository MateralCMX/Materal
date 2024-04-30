using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Materal.Abstractions;
using Materal.Logger;
using Materal.Tools.Core.LFConvert;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Materal.Tools.WinUI.ViewModels
{
    public partial class LFConvertViewModel : ObservableObject
    {
        /// <summary>
        /// 路径
        /// </summary>
        [ObservableProperty]
        private string _path = "D:\\Test";
        /// <summary>
        /// 文件名过滤器
        /// </summary>
        [ObservableProperty]
        private string _fileNameFilter = ".+\\.cs";
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
        private readonly ILFConvertService _LFConvertService;
        public LFConvertViewModel()
        {
            _loggerListener = App.ServiceProvider.GetRequiredService<ILoggerListener>();
            _loggerListener.Subscribe(OnLog);
            _LFConvertService = App.ServiceProvider.GetRequiredService<ILFConvertService>();
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
        private async Task LFToCRLFAsync()
        {
            LogMessages.Clear();
            LFConvertOptions options = new()
            {
                Filter = fileInfo => new Regex(FileNameFilter).Match(fileInfo.Name).Success,
                Recursive = Recursive
            };
            await _LFConvertService.LFToCRLFAsync(Path, options);
        }
        [RelayCommand]
        private async Task CRLFToLFAsync()
        {
            LogMessages.Clear();
            LFConvertOptions options = new()
            {
                Filter = fileInfo => new Regex(FileNameFilter).Match(fileInfo.Name).Success,
                Recursive = Recursive
            };
            await _LFConvertService.CRLFToLFAsync(Path, options);
        }
    }
}
