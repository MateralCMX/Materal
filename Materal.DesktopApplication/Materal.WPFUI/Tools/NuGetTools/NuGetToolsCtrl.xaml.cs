using System.Windows.Controls;
using Materal.WPFUI.Tools.NuGetTools.Model;

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
            ProjectSearchBox.SearchFun = m =>
            {
                if (!(m is NuGetToolsConfigTemplateModel model)) return false;
                if (string.IsNullOrEmpty(ViewModel.ProjectAddress)) return true;
                return model.ProjectAddress.Contains(ViewModel.ProjectAddress);
            };
            ProjectSearchBox.SelectedFun = m =>
            {
                if (!(m is NuGetToolsConfigTemplateModel model)) return false;
                if (string.IsNullOrEmpty(ViewModel.ProjectAddress)) return false;
                return model.ProjectAddress.Equals(ProjectSearchBox.Text);
            };
            ViewModel.Init();
        }
    }
}
