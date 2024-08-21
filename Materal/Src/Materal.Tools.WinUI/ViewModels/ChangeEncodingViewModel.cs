using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Materal.Tools.Core.ChangeEncoding;
using Materal.Tools.WinUI;
using Microsoft.Extensions.DependencyInjection;
using System;
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
        private string _path = "D:\\Test";
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
        private bool _forcedRead = false;
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
        private readonly IChangeEncodingService _changeEncodingService;
        public event Action? OnClearMessage;
        public ChangeEncodingViewModel()
        {
            _changeEncodingService = App.ServiceProvider.GetRequiredService<IChangeEncodingService>();
        }
        [RelayCommand]
        private async Task ChangeEncodingAsync()
        {
            OnClearMessage?.Invoke();
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
