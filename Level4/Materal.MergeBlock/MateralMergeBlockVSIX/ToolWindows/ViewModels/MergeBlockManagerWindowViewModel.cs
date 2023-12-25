using System.ComponentModel;
using System.Windows.Controls;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class MergeBlockManagerWindowViewModel : BaseViewModel
    {
        private bool _solutionIsOpen = false;
        /// <summary>
        /// 解决方案是否打开
        /// </summary>
        public bool SolutionIsOpen
        {
            get => _solutionIsOpen;
            set
            {
                _solutionIsOpen = value;
                OnPropertyChanged(nameof(SolutionIsOpen));
            }
        }
        private UserControl _currentControl = new SolutionNotOpenedControl();
        /// <summary>
        /// 当前控件
        /// </summary>
        public UserControl CurrentControl
        {
            get => _currentControl;
            set
            {
                _currentControl = value;
                OnPropertyChanged(nameof(CurrentControl));
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
        {
            PropertyChanged += MergeBlockManagerWindowViewModel_PropertyChanged;
            VS.Events.SolutionEvents.OnAfterOpenSolution += SolutionEvents_OnAfterOpenSolution;
            VS.Events.SolutionEvents.OnAfterCloseSolution += SolutionEvents_OnAfterCloseSolution;
            SolutionIsOpen = await VS.Solutions.IsOpenAsync();
        }
        private void MergeBlockManagerWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SolutionIsOpen))
            {
                CurrentControl = SolutionIsOpen ? new SolutionOpenedControl() : new SolutionNotOpenedControl();
            }
        }
        private void SolutionEvents_OnAfterCloseSolution() => SolutionIsOpen = false;
        private void SolutionEvents_OnAfterOpenSolution(Solution obj) => SolutionIsOpen = true;
    }
}
