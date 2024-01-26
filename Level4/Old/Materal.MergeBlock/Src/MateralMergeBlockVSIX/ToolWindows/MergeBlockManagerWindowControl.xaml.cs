using System.Windows;
using System.Windows.Controls;

namespace MateralMergeBlockVSIX
{
    public partial class MergeBlockManagerWindowControl : UserControl
    {
        public MergeBlockManagerWindowControl() => InitializeComponent();

        private void UserControl_Loaded(object sender, RoutedEventArgs e) => ViewModel.InitAsync().FireAndForget();
    }
}