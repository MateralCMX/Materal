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
            //DataContext = new SolutionOpenedControlViewModel();
        }
    }
}
