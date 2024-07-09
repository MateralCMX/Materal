using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Materal.Tools.Core;
using Materal.Tools.Core.MateralVersion;
using Microsoft.Extensions.DependencyInjection;
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
        public event Action<MessageLevel, string?>? OnMessage;
        public event Action? OnClearMessage;
        public MateralVersionViewModel()
        {
            _versionService = App.ServiceProvider.GetRequiredService<IMateralVersionService>();
        }
        [RelayCommand]
        private async Task UpdateVersionAsync()
        {
            OnClearMessage?.Invoke();
            try
            {
                if (string.IsNullOrWhiteSpace(Version))
                {
                    string[] nugetPaths = {
                        "https://nuget.gudianbustu.com/nuget/",
                        @"E:\Project\Materal\Materal\Nupkgs"
                    };
                    await _versionService.UpdateVersionAsync(ProjectPath, nugetPaths, OnMessage);
                }
                else
                {
                    await _versionService.UpdateVersionAsync(ProjectPath, Version, OnMessage);
                }
            }
            catch (Exception ex)
            {
                OnMessage?.Invoke(MessageLevel.Error, ex.Message);
                OnMessage?.Invoke(MessageLevel.Error, ex.StackTrace);
            }
        }
    }
}
