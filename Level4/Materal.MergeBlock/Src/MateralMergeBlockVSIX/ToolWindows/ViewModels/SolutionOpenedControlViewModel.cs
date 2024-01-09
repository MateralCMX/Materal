#nullable enable
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        /// <summary>
        /// 生成代码
        /// </summary>
        public void GeneratorCode()
        {
            try
            {
                #region 清理旧文件
                DirectoryInfo? directoryInfo = _moduleAbstractions?.GetGeneratorCodeRootDirectory();
                if (directoryInfo is not null && directoryInfo.Exists)
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
                List<DomainModel> allDomains = _moduleAbstractions?.GetAllDomains() ?? [];
                IEnumerable<MethodInfo> allMethodInfos = GetType().GetRuntimeMethods();
                foreach (MethodInfo methodInfo in allMethodInfos)
                {
                    if (methodInfo.GetCustomAttribute<GeneratorCodeMethodAttribute>() is null) continue;
                    ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                    if (parameterInfos.Length == 0)
                    {
                        methodInfo.Invoke(this, []);
                    }
                    else if(parameterInfos.Length == 1 && parameterInfos.First().ParameterType == typeof(List<DomainModel>))
                    {
                        methodInfo.Invoke(this, [allDomains]);
                    }
                }
                VS.MessageBox.Show("提示", "代码生成完毕", Microsoft.VisualStudio.Shell.Interop.OLEMSGICON.OLEMSGICON_INFO, Microsoft.VisualStudio.Shell.Interop.OLEMSGBUTTON.OLEMSGBUTTON_OK);
            }
            catch (Exception ex)
            {
                VS.MessageBox.Show("错误", ex.GetErrorMessage(), Microsoft.VisualStudio.Shell.Interop.OLEMSGICON.OLEMSGICON_WARNING, Microsoft.VisualStudio.Shell.Interop.OLEMSGBUTTON.OLEMSGBUTTON_OK);
            }
        }
    }
}
