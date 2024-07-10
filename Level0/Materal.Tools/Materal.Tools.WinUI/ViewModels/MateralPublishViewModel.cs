using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Materal.Tools.Core;
using Materal.Tools.Core.MateralPublish;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Materal.Tools.WinUI.ViewModels
{
    public partial class MateralPublishViewModel : ObservableObject
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
        private string _version = "1.0.0";
        public ObservableCollection<MateralProjectViewModel> Projects { get; } = [];
        private readonly IMateralPublishService _publishService;
        private readonly ILogger<MateralVersionViewModel> _logger;
        public event Action? OnClearMessage;
        public MateralPublishViewModel()
        {
            _publishService = App.ServiceProvider.GetRequiredService<IMateralPublishService>();
            _logger = App.ServiceProvider.GetRequiredService<ILogger<MateralVersionViewModel>>();
            MateralProjectViewModel[] allProjects = _publishService.GetAllProjects().Select(m => new MateralProjectViewModel(m)).ToArray();
            Projects.Clear();
            foreach (MateralProjectViewModel project in allProjects)
            {
                Projects.Add(project);
            }
            if (_publishService.IsMateralProjectPath(ProjectPath))
            {
                Version = _publishService.GetNowVersion(ProjectPath);
            }
            else
            {
                ProjectPath = string.Empty;
            }
        }
        partial void OnProjectPathChanged(string? oldValue, string newValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ProjectPath))
                {
                    Version = "1.0.0";
                    return;
                }
                if (!_publishService.IsMateralProjectPath(ProjectPath)) throw new ToolsException($"\"{ProjectPath}\"不是Materal项目路径");
                Version = _publishService.GetNowVersion(ProjectPath);
            }
            catch (Exception)
            {
                ProjectPath = oldValue ?? string.Empty;
                throw;
            }
        }
        [RelayCommand]
        private void SelectionAllProject()
        {
            foreach (MateralProjectViewModel project in Projects)
            {
                project.IsPublish = true;
            }
        }
        [RelayCommand]
        private void InvertSelectionProject()
        {
            foreach (MateralProjectViewModel project in Projects)
            {
                project.IsPublish = !project.IsPublish;
            }
        }
        [RelayCommand]
        private async Task PublishAsync()
        {
            OnClearMessage?.Invoke();
            try
            {
                IMateralProject[] projects = Projects.Where(m => m.IsPublish).Select(m => m.MateralProject).ToArray();
                await _publishService.PublishAsync(ProjectPath, Version, projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
