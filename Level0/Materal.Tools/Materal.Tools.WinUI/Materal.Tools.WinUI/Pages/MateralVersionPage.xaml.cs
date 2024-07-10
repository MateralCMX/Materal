using Materal.Tools.WinUI.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Materal.Tools.WinUI.Pages
{
    [Menu("Materal版本", "\uF0B3", 1)]
    public sealed partial class MateralVersionPage : Page
    {
        public MateralVersionViewModel ViewModel { get; } = new();
        public MateralVersionPage()
        {
            ViewModel.OnClearMessage += ViewModel_OnClearMessage;
            InitializeComponent();
        }
        private void ViewModel_OnClearMessage() => logPrint.ClearLogs();
    }
}
