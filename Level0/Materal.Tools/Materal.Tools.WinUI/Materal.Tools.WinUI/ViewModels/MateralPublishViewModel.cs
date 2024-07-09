﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Materal.Tools.Core;
using Materal.Tools.Core.MateralPublish;
using Microsoft.Extensions.DependencyInjection;
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
        public event Action<MessageLevel, string?>? OnMessage;
        public event Action? OnClearMessage;
        public MateralPublishViewModel()
        {
            _publishService = App.ServiceProvider.GetRequiredService<IMateralPublishService>();
            ReloadProjectAsync();
        }
        /// <summary>
        /// 重新加载项目
        /// </summary>
        private async void ReloadProjectAsync()
        {
            Version = await _publishService.GetNowVersionAsync(ProjectPath);
            MateralProjectViewModel[] allProjects = _publishService.GetAllProjects().Select(m => new MateralProjectViewModel(m)).ToArray();
            Projects.Clear();
            foreach (MateralProjectViewModel project in allProjects)
            {
                Projects.Add(project);
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
                await _publishService.PublishAsync(ProjectPath, Version, projects, OnMessage);
            }
            catch (Exception ex)
            {
                OnMessage?.Invoke(MessageLevel.Error, ex.Message);
                OnMessage?.Invoke(MessageLevel.Error, ex.StackTrace);
            }
        }
    }
}
