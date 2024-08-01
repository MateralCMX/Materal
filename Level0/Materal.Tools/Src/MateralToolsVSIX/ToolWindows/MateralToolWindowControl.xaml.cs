using System.Windows;
using System.Windows.Controls;

namespace MateralToolsVSIX.ToolWindows
{
    public partial class MateralToolWindowControl : UserControl
    {
        public MateralToolWindowControl() => InitializeComponent();
        private void BtnInsertGuid_Click(object sender, RoutedEventArgs e)
            => ThreadHelper.JoinableTaskFactory.RunAsync(ViewModel.InsertNewGuid.InsertNewGuidStringAsync);

        private void BtnChangePackVersion_Click(object sender, RoutedEventArgs e)
            => ThreadHelper.JoinableTaskFactory.RunAsync(ViewModel.ChangePackVersion.ChangePackVersionAsync);
    }
}