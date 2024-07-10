using Materal.Tools.Core;
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
            ViewModel.OnMessage += ViewModel_OnMessage;
            ViewModel.OnClearMessage += ViewModel_OnClearMessage;
            InitializeComponent();
        }
        private void ViewModel_OnClearMessage() => consolePrint.ClearMessage();
        private void ViewModel_OnMessage(MessageLevel level, string? message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            consolePrint.AddMessage(level, message);
        }
    }
}
