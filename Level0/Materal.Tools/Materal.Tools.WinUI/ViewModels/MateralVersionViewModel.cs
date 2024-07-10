using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Materal.Tools.Core.MateralVersion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Materal.Tools.WinUI.ViewModels
{
    public partial class MateralVersionViewModel : ObservableObject
    {
        /// <summary>
        /// 项目路径
        /// </summary>
        [ObservableProperty]
        private string _projectPath = @"E:\Project\Materal\Materal";
        /// <summary>
        /// 版本号
        /// </summary>
        [ObservableProperty]
        private string _version = string.Empty;
        private readonly IMateralVersionService _versionService;
        private readonly ILogger<MateralVersionViewModel> _logger;
        public event Action? OnClearMessage;
        public MateralVersionViewModel()
        {
            _versionService = App.ServiceProvider.GetRequiredService<IMateralVersionService>();
            _logger = App.ServiceProvider.GetRequiredService<ILogger<MateralVersionViewModel>>();
        }
        [RelayCommand]
        private async Task UpdateVersionAsync()
        {
            OnClearMessage?.Invoke();
            try
            {
                if (string.IsNullOrWhiteSpace(Version))
                {
                    await _versionService.UpdateVersionAsync(ProjectPath);
                }
                else
                {
                    await _versionService.UpdateVersionAsync(ProjectPath, Version);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
