#nullable enable
using Materal.Abstractions;
using MateralMergeBlockVSIX.ToolWindows.ViewModels;
using Microsoft.VisualStudio.Shell.Interop;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                List<string> errorList = [];
                foreach (ModuleViewModel moduleViewModel in ViewModel.Modules)
                {
                    moduleViewModel.Build();
                    if (!moduleViewModel.BuildSuccess)
                    {
                        errorList.Add(moduleViewModel.ModuleName);
                    }
                }
                if (errorList.Count == 0)
                {
                    VS.MessageBox.Show("提示", "所有模块已构建", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK);
                }
                else
                {
                    VS.MessageBox.Show("提示", $"模块{string.Join(",", errorList)}构建失败", OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK);
                }
            }
            catch (Exception ex)
            {
                VS.MessageBox.Show("错误", ex.GetErrorMessage(), OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK);
            }
        }
        private void OpenModuleSolution_Click(object sender, RoutedEventArgs e)
        {
            if(sender is not Button button || button.DataContext is not ModuleViewModel viewModel) return;
            viewModel.Open();
        }
        private void BuildModuleSolution_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is not Button button || button.DataContext is not ModuleViewModel viewModel) return;
                viewModel.Build();
                if (viewModel.BuildSuccess)
                {
                    VS.MessageBox.Show("提示", "项目已构建", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK);
                }
                else
                {
                    VS.MessageBox.Show("错误", "项目构建失败", OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK);
                }
            }
            catch (Exception ex)
            {
                VS.MessageBox.Show("错误", ex.GetErrorMessage(), OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK);
            }
        }
    }
}
