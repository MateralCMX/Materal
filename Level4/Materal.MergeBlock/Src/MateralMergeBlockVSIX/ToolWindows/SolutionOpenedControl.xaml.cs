#nullable enable
using System.Windows.Controls;

namespace MateralMergeBlockVSIX.ToolWindows
{
    public partial class SolutionOpenedControl : UserControl
    {
        public SolutionOpenedControl()
        {
            InitializeComponent();
        }
        public void Init(Solution? solution)
        {
            if (solution is null) return;
            ViewModel.Init(solution);
        }
        private void GeneratorCodeButton_Click(object sender, System.Windows.RoutedEventArgs e) => ThreadHelper.JoinableTaskFactory.Run(ViewModel.GeneratorCodeAsync);
    }
}
