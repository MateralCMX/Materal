using Materal.Tools.WinUI.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Materal.Tools.WinUI.Pages
{
    [Menu("项目清理", "\uED62", 2)]
    public sealed partial class ProjectClearPage : Page
    {
        public ProjectClearViewModel ViewModel { get; } = new();
        public ProjectClearPage()
        {
            ViewModel.OnClearMessage += ViewModel_OnClearMessage;
            InitializeComponent();
        }
        private void ViewModel_OnClearMessage() => logPrint.ClearLogs();
    }
}
