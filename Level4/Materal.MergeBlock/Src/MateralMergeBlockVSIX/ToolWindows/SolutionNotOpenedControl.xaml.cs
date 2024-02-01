using System.Windows;
using System.Windows.Forms;
using UserControl = System.Windows.Controls.UserControl;

namespace MateralMergeBlockVSIX.ToolWindows
{
    public partial class SolutionNotOpenedControl : UserControl
    {
        public SolutionNotOpenedControl() => InitializeComponent();
        private void ChangeProjectPathButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new()
            {
                SelectedPath = ViewModel.ProjectPath
            };
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            ViewModel.ProjectPath = folderBrowserDialog.SelectedPath;
        }
        private void CreateModuleButton_Click(object sender, RoutedEventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ViewModel.CreateModule();
        }
    }
}
