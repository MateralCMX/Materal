using CommunityToolkit.Mvvm.ComponentModel;
using Materal.Tools.Core.MateralPublish;

namespace Materal.Tools.WinUI.ViewModels
{
    public partial class MateralProjectViewModel(IMateralProject materalProject) : ObservableObject
    {
        /// <summary>
        /// Materal项目
        /// </summary>
        public IMateralProject MateralProject { get; } = materalProject;
        [ObservableProperty]
        private bool _isPublish = false;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name => MateralProject.Name;
    }
}
