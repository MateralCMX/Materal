using CommunityToolkit.Mvvm.ComponentModel;
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
        public ObservableCollection<MateralProjectViewModel> Projects = [];
        private readonly IMateralPublishService _publishService;
        public ObservableCollection<MessageViewModel> Messages = [];
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
            MateralProjectViewModel[] allProjects = _publishService.GetAllProjects(ProjectPath).Select(m => new MateralProjectViewModel(m)).ToArray();
            Projects.Clear();
            foreach (MateralProjectViewModel project in allProjects)
            {
                Projects.Add(project);
            }
        }
        [RelayCommand]
        private async Task PublishAsync()
        {
            Messages.Clear();
            try
            {
                OnMessage(MessageLevel.Information, "开始发布...");
                foreach (MateralProjectViewModel project in Projects.Where(m => m.IsPublish))
                {
                    OnMessage(MessageLevel.Information, $"开始发布{project.Name}...");
                    await project.MateralProject.PublishAsync(Version, OnMessage);
                    OnMessage(MessageLevel.Information, $"{project.Name}发布完毕");
                }
            }
            catch (Exception ex)
            {
                OnMessage(MessageLevel.Error, ex.Message);
                if (!string.IsNullOrWhiteSpace(ex.StackTrace))
                {
                    OnMessage(MessageLevel.Error, ex.StackTrace);
                }
            }
        }
        private void OnMessage(MessageLevel level, string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            Messages.Add(new(level, message));
        }
    }
}
