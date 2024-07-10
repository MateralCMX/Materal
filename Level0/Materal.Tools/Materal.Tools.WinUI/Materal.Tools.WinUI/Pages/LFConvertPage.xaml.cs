using Materal.Tools.WinUI.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Materal.Tools.WinUI.Pages
{
    [Menu("LF转换", "\uE71D")]
    public sealed partial class LFConvertPage : Page
    {
        public LFConvertViewModel ViewModel { get; } = new();
        public LFConvertPage()
        {
            ViewModel.OnClearMessage += ViewModel_OnClearMessage;
            InitializeComponent();
        }
        private void ViewModel_OnClearMessage() => logPrint.ClearLogs();
    }
}
