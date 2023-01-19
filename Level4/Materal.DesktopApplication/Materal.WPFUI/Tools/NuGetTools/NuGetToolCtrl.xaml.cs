using Materal.StringHelper;
using Materal.WPFUI.Tools.NuGetTools.Model;
using System.Windows.Forms;

namespace Materal.WPFUI.Tools.NuGetTools
{
    /// <summary>
    /// NuGetToolCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class NuGetToolCtrl
    {
        public NuGetToolCtrl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ProjectSearchBox.SearchFun = m =>
            {
                if (!(m is NuGetToolsConfigTemplateModel model)) return false;
                return string.IsNullOrEmpty(ViewModel.ProjectAddress) || model.ProjectAddress.Contains(ViewModel.ProjectAddress);
            };
            ProjectSearchBox.SelectedFun = m =>
            {
                if (!(m is NuGetToolsConfigTemplateModel model)) return false;
                return !string.IsNullOrEmpty(ViewModel.ProjectAddress) && model.ProjectAddress.Equals(ProjectSearchBox.Text);
            };
            ViewModel.Init();
        }

        private void BrowseProjectAddressCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog
            {
                Description = @"选取项目文件夹",
                ShowNewFolderButton = false
            };
            if (!string.IsNullOrEmpty(ViewModel.ProjectAddress) && ViewModel.ProjectAddress.IsAbsoluteDirectoryPath())
            {
                folderBrowserDialog.SelectedPath = ViewModel.ProjectAddress;
            }
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ViewModel.ProjectAddress = folderBrowserDialog.SelectedPath;
            }
        }

        private void BrowseTargetAddressCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog
            {
                Description = @"选取目标文件夹",
                ShowNewFolderButton = false
            };
            if (!string.IsNullOrEmpty(ViewModel.TargetAddress) && ViewModel.TargetAddress.IsAbsoluteDirectoryPath())
            {
                folderBrowserDialog.SelectedPath = ViewModel.TargetAddress;
            }
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ViewModel.TargetAddress = folderBrowserDialog.SelectedPath;
            }
        }

        private void ExportCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ViewModel.Export();
        }

        private void ExportCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel.CanExport && !ViewModel.Exporting;
        }
    }
}
