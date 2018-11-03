using System.Windows.Controls;

namespace Materal.WPFUI.Tools.NuGetTools
{
    /// <summary>
    /// NuGetToolsCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class NuGetToolsCtrl : UserControl
    {
        public NuGetToolsCtrl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.Init();
        }
    }
}
