#nullable enable
using MateralToolsVSIX.ToolWindows.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MateralToolsVSIX.ToolWindows
{
    public partial class MateralToolWindowControl : UserControl
    {
        public MateralToolViewModel ViewModel { get; } = new();
        public MateralToolWindowControl()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
        private void BtnInsertGuid_Click(object sender, RoutedEventArgs e)
            => ThreadHelper.JoinableTaskFactory.Run(ViewModel.InsertNewGuid.InsertNewGuidStringAsync);
    }
}