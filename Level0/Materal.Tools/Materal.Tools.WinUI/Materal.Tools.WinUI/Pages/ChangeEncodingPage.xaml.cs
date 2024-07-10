using MateralTools.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Materal.Tools.WinUI.Pages
{
    /// <summary>
    /// 更改编码
    /// </summary>
    [Menu("更改编码", "\uE71D")]
    public sealed partial class ChageEncodingPage : Page
    {
        public ChangeEncodingViewModel ViewModel { get; } = new();
        public ChageEncodingPage()
        {
            ViewModel.OnClearMessage += ViewModel_OnClearMessage;
            InitializeComponent();
        }
        private void ViewModel_OnClearMessage() => logPrint.ClearLogs();
    }
}
