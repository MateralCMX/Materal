#nullable enable
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedControlViewModel : ObservableObject
    {
        private string _solutionName = "不是MergeBlock模块项目";
        /// <summary>
        /// 解决方案名称
        /// </summary>
        public string SolutionName { get => _solutionName; set { _solutionName = value; NotifyPropertyChanged(); } }
        private Visibility _visibility = Visibility.Collapsed;
        /// <summary>
        /// 显示状态
        /// </summary>
        public Visibility Visibility { get => _visibility; set { _visibility = value; NotifyPropertyChanged(); } }
        private string? _projectName;
        private string? _moduleName;
        private SolutionItem? _coreAbstractions;
        private SolutionItem? _coreReposiroty;
        private SolutionItem? _moduleAbstractions;
        private SolutionItem? _moduleApplication;
        private SolutionItem? _moduleReposiroty;
        private SolutionItem? _moduleWebAPI;
        public void Init(Solution solution)
        {
            try
            {
                BindingSolutionItems(solution.Children);
                SolutionName = $"{_projectName}.{_moduleName}";
                Visibility = Visibility.Visible;
            }
            catch
            {
                SolutionName = "不是MergeBlock模块项目";
                Visibility = Visibility.Collapsed;
                _coreAbstractions = null;
                _coreReposiroty = null;
                _moduleAbstractions = null;
                _moduleApplication = null;
                _moduleReposiroty = null;
                _moduleWebAPI = null;
            }
        }
        private void BindingSolutionItems(IEnumerable<SolutionItem?> solutionItems)
        {
            foreach (SolutionItem? solutionItem in solutionItems)
            {
                if (solutionItem is null) continue;
                BindingSolutionItem(solutionItem);
            }
        }
        private void BindingSolutionItem(SolutionItem solutionItem)
        {
            if (solutionItem.Type == SolutionItemType.SolutionFolder)
            {
                BindingSolutionItems(solutionItem.Children);
            }
            else if (solutionItem.Type == SolutionItemType.Project)
            {
                string[] projectNames = solutionItem.Name.Split('.');
                if (projectNames.Length != 3) return;
                if (_projectName is null)
                {
                    _projectName = projectNames[0];
                }
                else if (_projectName != projectNames[0]) throw new Exception("项目名称不一致");
                if (projectNames[1] != "Core")
                {
                    if (_moduleName is null)
                    {
                        _moduleName = projectNames[1];
                    }
                    else if (_moduleName != projectNames[1]) throw new Exception("模块名称不一致");
                    switch (projectNames[2])
                    {
                        case "WebAPI":
                            _moduleWebAPI = solutionItem;
                            break;
                        case "Abstractions":
                            _moduleAbstractions = solutionItem;
                            break;
                        case "Application":
                            _moduleApplication = solutionItem;
                            break;
                        case "Repository":
                            _moduleReposiroty = solutionItem;
                            break;
                    }
                }
                else
                {
                    switch (projectNames[2])
                    {
                        case "Abstractions":
                            _coreAbstractions = solutionItem;
                            break;
                        case "Repository":
                            _coreReposiroty = solutionItem;
                            break;
                    }
                }
            }
        }
        #region 生成代码
        /// <summary>
        /// 生成代码
        /// </summary>
        public void GeneratorCode()
        {
            if (_moduleAbstractions is null) return;
            #region 清理旧文件
            DirectoryInfo? directoryInfo = _moduleAbstractions.GetGeneratorCodeRootDirectory();
            if (directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }
            directoryInfo = _moduleApplication?.GetGeneratorCodeRootDirectory();
            if (directoryInfo is not null && directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }
            directoryInfo = _moduleReposiroty?.GetGeneratorCodeRootDirectory();
            if (directoryInfo is not null && directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }
            directoryInfo = _moduleWebAPI?.GetGeneratorCodeRootDirectory();
            if (directoryInfo is not null && directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }
            #endregion
            GeneratorUnitOfWorkCode();
            List<DomainModel> allDomains = _moduleAbstractions.GetAllDomains();
            GeneratorEntityConfigsCode(allDomains);
            GeneratorDBContextCode(allDomains);
            GeneratorRepositoryCode(allDomains);
        }
        #endregion
    }
}
