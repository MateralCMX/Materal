﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Materal.Tools.Core.ProjectClear;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Materal.Tools.WinUI.ViewModels
{
    public partial class ProjectClearViewModel : ObservableObject
    {
        /// <summary>
        /// 项目路径
        /// </summary>
        [ObservableProperty]
        private string _projectPath = @"D:\Test";
        private readonly IProjectClearService _projectClearService;
        private readonly ILogger<ProjectClearViewModel> _logger;
        public event Action? OnClearMessage;
        public ProjectClearViewModel()
        {
            _projectClearService = App.ServiceProvider.GetRequiredService<IProjectClearService>();
            _logger = App.ServiceProvider.GetRequiredService<ILogger<ProjectClearViewModel>>();
        }
        [RelayCommand]
        private void ProjectClear()
        {
            OnClearMessage?.Invoke();
            try
            {
                _projectClearService.ClearProject(ProjectPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}