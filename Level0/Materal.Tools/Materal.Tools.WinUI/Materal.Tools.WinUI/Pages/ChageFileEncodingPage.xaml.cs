using MateralTools.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Materal.Tools.WinUI.Pages
{
    /// <summary>
    /// 更改文件编码
    /// </summary>
    [Menu("更改文件编码", "\uE71D")]
    public sealed partial class ChageFileEncodingPage : Page
    {
        public ChageFileEncodingViewModel ViewModel { get; } = new();
        public ChageFileEncodingPage() => InitializeComponent();
    }
}
