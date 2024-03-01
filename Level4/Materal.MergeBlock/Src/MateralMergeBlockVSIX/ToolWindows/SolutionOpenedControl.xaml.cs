#nullable enable
using Materal.Abstractions;
using MateralMergeBlockVSIX.ToolWindows.ViewModels;
using Microsoft.VisualStudio.Shell.Interop;
using System.Windows;
using System.Windows.Controls;

namespace MateralMergeBlockVSIX.ToolWindows
{
    public partial class SolutionOpenedControl : UserControl
    {
        public SolutionOpenedControl() => InitializeComponent();
        public void Init(Solution? solution)
        {
            if (solution is null) return;
            ViewModel.Init(solution);
        }
        private void GeneratorCodeButton_Click(object sender, RoutedEventArgs e) => ThreadHelper.JoinableTaskFactory.Run(ViewModel.GeneratorCodeAsync);
        private void CreateNewModuleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ThreadHelper.ThrowIfNotOnUIThread();
                SolutionNotOpenedViewModel viewModel = new()
                {
                    ModuleName = ModuleName.Text,
                    ProjectName = ViewModel.ProjectName,
                    ProjectPath = ViewModel.ProjectPath ?? string.Empty
                };
                if (string.IsNullOrWhiteSpace(ViewModel.ProjectName) || string.IsNullOrWhiteSpace(ViewModel.ProjectPath)) return;
                viewModel.CreateModule();
            }
            catch (Exception ex)
            {
                VS.MessageBox.Show("错误", ex.GetErrorMessage(), OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK);
            }
        }
        private void BuildAllButton_Click(object sender, RoutedEventArgs e)
        {
            ThreadHelper.JoinableTaskFactory.Run(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                IVsThreadedWaitDialogFactory fac = (IVsThreadedWaitDialogFactory)await VS.Services.GetThreadedWaitDialogAsync();
                IVsThreadedWaitDialog4 twd = fac.CreateInstance();
                int totalStep = ViewModel.Modules.Count;
                SetBuildText(twd, totalStep);
                int index = 1;
                foreach (ModuleViewModel moduleViewModel in ViewModel.Modules)
                {
                    SetBuildText(twd, moduleViewModel, index++, totalStep);
                    await moduleViewModel.BuildAsync();
                }
                twd.EndWaitDialog(out _);
            });
        }
        private void OpenModuleSolution_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button || button.DataContext is not ModuleViewModel viewModel) return;
            ThreadHelper.ThrowIfNotOnUIThread();
            viewModel.Open();
        }
        private void BuildModuleSolution_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button || button.DataContext is not ModuleViewModel viewModel) return;
            ThreadHelper.JoinableTaskFactory.Run(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                IVsThreadedWaitDialogFactory fac = (IVsThreadedWaitDialogFactory)await VS.Services.GetThreadedWaitDialogAsync();
                IVsThreadedWaitDialog4 twd = fac.CreateInstance();
                const int totalStep = 1;
                SetBuildText(twd, totalStep);
                SetBuildText(twd, viewModel, totalStep, totalStep);
                await viewModel.BuildAsync();
                twd.EndWaitDialog(out _);
            });
        }
        /// <summary>
        /// 设置构建文本
        /// </summary>
        /// <param name="twd"></param>
        /// <param name="viewModel"></param>
        /// <param name="currentStep"></param>
        /// <param name="totalStep"></param>
        private void SetBuildText(IVsThreadedWaitDialog4 twd, ModuleViewModel viewModel, int currentStep, int totalStep)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string message = $"正在构建{viewModel.ModuleName}...";
            string stepInfo = $"步骤 {currentStep}/{totalStep}";
            twd.UpdateProgress(message, stepInfo, stepInfo, currentStep, totalStep, true, out _);
        }
        /// <summary>
        /// 设置构建文本
        /// </summary>
        /// <param name="twd"></param>
        /// <param name="viewModel"></param>
        /// <param name="currentStep"></param>
        /// <param name="totalStep"></param>
        private void SetBuildText(IVsThreadedWaitDialog4 twd, int totalStep)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string message = "准备开始任务...";
            string stepInfo = $"步骤 1/{totalStep}";
            twd.StartWaitDialog("MergeBlockBuild", message, stepInfo, null, stepInfo, 1, true, true);
        }
    }
}
