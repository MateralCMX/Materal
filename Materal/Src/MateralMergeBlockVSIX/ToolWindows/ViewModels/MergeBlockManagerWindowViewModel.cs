#nullable enable
using Microsoft.VisualStudio.PlatformUI;
using System.Windows.Controls;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class MergeBlockManagerWindowViewModel : ObservableObject
    {
        private UserControl? _currentControl = new SolutionNotOpenedControl();
        /// <summary>
        /// 当前控件
        /// </summary>
        public UserControl? CurrentControl { get => _currentControl; set { _currentControl = value; NotifyPropertyChanged(); } }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
        {
            VS.Events.SolutionEvents.OnAfterOpenSolution += SolutionEvents_OnAfterOpenSolution;
            VS.Events.SolutionEvents.OnAfterCloseSolution += SolutionEvents_OnAfterCloseSolution;
            if (await VS.Solutions.IsOpenAsync())
            {
                CurrentControl = GetSolutionOpenedControl();
            }
            else
            {
                CurrentControl = GetSolutionNotOpenedControl();
            }
        }
        private void SolutionEvents_OnAfterCloseSolution() => CurrentControl = GetSolutionNotOpenedControl();
        private void SolutionEvents_OnAfterOpenSolution(Solution? solution) => CurrentControl = GetSolutionOpenedControl(solution);
        private UserControl GetSolutionNotOpenedControl()
        {
            SolutionNotOpenedControl control = new();
            return control;
        }
        private UserControl GetSolutionOpenedControl(Solution? solution = null)
        {
            solution ??= VS.Solutions.GetCurrentSolution();
            SolutionOpenedControl control = new();
            control.Init(solution);
            return control;
        }
    }
}
