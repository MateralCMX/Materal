using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Materal.Tools.Core.ChangeEncoding;
using Materal.Tools.WinUI;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.RegularExpressions;

namespace MateralTools.ViewModels
{
    public partial class ChangeEncodingViewModel : ObservableObject
    {
        /// <summary>
        /// 路径
        /// </summary>
        [ObservableProperty]
        public partial string Path { get; set; } = "D:\\Test";
        /// <summary>
        /// 编码
        /// </summary>
        [ObservableProperty]
        public partial string EncodingName { get; set; } = "UTF-8";
        /// <summary>
        /// 文件名过滤器
        /// </summary>
        [ObservableProperty]
        public partial string FileNameFilter { get; set; } = "^.+\\.cs$";
        /// <summary>
        /// 强制读取
        /// </summary>
        [ObservableProperty]
        public partial bool ForcedRead { get; set; } = false;
        /// <summary>
        /// 读取编码
        /// </summary>
        [ObservableProperty]
        public partial string ReadEncodingName { get; set; } = "GBK";
        /// <summary>
        /// 递归
        /// </summary>
        [ObservableProperty]
        public partial bool Recursive { get; set; } = true;
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
