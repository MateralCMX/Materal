#nullable enable
using Materal.Abstractions;
using Materal.MergeBlock.GeneratorCode;
using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Extensions;
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell.Interop;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedViewModel : ObservableObject
    {
        private string _solutionName = "不是MergeBlock项目";
        /// <summary>
        /// 解决方案名称
        /// </summary>
        public string SolutionName { get => _solutionName; set { _solutionName = value; NotifyPropertyChanged(); } }
        private Visibility _visibility = Visibility.Collapsed;
        /// <summary>
        /// 显示状态
        /// </summary>
        public Visibility Visibility { get => _visibility; set { _visibility = value; NotifyPropertyChanged(); } }
        /// <summary>
        /// 模块
        /// </summary>
        public ObservableCollection<ModuleViewModel> Modules { get; set; } = [];
        private GeneratorCodeContext? _context;
        public GeneratorCodeContext Context { get => _context ?? throw new Exception("上下文为空"); set => _context = value; }
        /// <summary>
        /// 生成代码
        /// </summary>
        public async Task GeneratorCodeAsync()
        {
            try
            {
                List<string> generatorCodePlugPaths = GetAllGeneratorCodePlugPaths();
                List<string> editGeneratorCodePlugPaths = GetAllEditGeneratorCodePlugPaths();
                List<IMergeBlockEditGeneratorCodePlug> editGeneratorCodePlugs = [];
                foreach (string codePath in editGeneratorCodePlugPaths)
                {
                    try
                    {
                        IMergeBlockEditGeneratorCodePlug plug = await GetMergeBlockEditGeneratorCodePlugAsync(codePath);
                        editGeneratorCodePlugs.Add(plug);
                    }
                    catch (Exception ex)
                    {
                        await VS.MessageBox.ShowAsync($"插件[{codePath}]错误", ex.GetErrorMessage(), OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK);
                    }
                }
                int stepCount = generatorCodePlugPaths.Count > 0 ? 3 : 2;
                await VS.StatusBar.ShowProgressAsync($"清理以前生成的文件 1/{stepCount}", 1, stepCount);
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
                directoryInfo = _moduleRepository?.GetGeneratorCodeRootDirectory();
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
                IEnumerable<MethodInfo> allMethodInfos = GetType().GetRuntimeMethods();
                string coreAbstractionsPath = Path.GetDirectoryName(_coreAbstractions?.FullPath ?? throw new Exception("获取coreAbstractions路径失败"));
                string coreRepositoryPath = Path.GetDirectoryName(_coreRepository?.FullPath ?? throw new Exception("获取coreRepository路径失败"));
                string moduleAbstractionsPath = Path.GetDirectoryName(_moduleAbstractions?.FullPath ?? throw new Exception("获取moduleAbstractions路径失败"));
                string moduleApplicationPath = Path.GetDirectoryName(_moduleApplication?.FullPath ?? throw new Exception("获取moduleApplication路径失败"));
                string moduleRepositoryPath = Path.GetDirectoryName(_moduleRepository?.FullPath ?? throw new Exception("获取moduleRepository路径失败"));
                string moduleWebAPIPath = Path.GetDirectoryName(_moduleWebAPI?.FullPath ?? throw new Exception("获取moduleWebAPI路径失败"));
                if (_projectName is null || string.IsNullOrWhiteSpace(_projectName)) throw new Exception("项目名称为空");
                if (_moduleName is null || string.IsNullOrWhiteSpace(_moduleName)) throw new Exception("模块名称为空");
                await VS.StatusBar.ShowProgressAsync($"生成预设代码 2/{stepCount}", 2, stepCount);
                Context = new(coreAbstractionsPath, coreRepositoryPath, moduleAbstractionsPath, moduleApplicationPath, moduleRepositoryPath, moduleWebAPIPath, _projectName, _moduleName, editGeneratorCodePlugs);
                Context.Refresh(_moduleAbstractions);
                await ExcuteMethodInfoByAttributeAsync<GeneratorCodeBeforMethodAttribute>(allMethodInfos);
                Context.Refresh(_moduleAbstractions);
                await ExcuteMethodInfoByAttributeAsync<GeneratorCodeMethodAttribute>(allMethodInfos);
                Context.Refresh(_moduleAbstractions);
                await ExcuteMethodInfoByAttributeAsync<GeneratorCodeAfterMethodAttribute>(allMethodInfos);
                Context.Refresh(_moduleAbstractions);
                if (generatorCodePlugPaths.Count > 0)
                {
                    await VS.StatusBar.ShowProgressAsync($"生成插件代码 3/{stepCount}", 3, stepCount);
                    await ExcutePlugAsync(generatorCodePlugPaths);
                }
                await VS.StatusBar.ShowMessageAsync("代码生成成功");
            }
            catch (Exception ex)
            {
                await VS.MessageBox.ShowAsync("错误", ex.GetErrorMessage(), OLEMSGICON.OLEMSGICON_WARNING, OLEMSGBUTTON.OLEMSGBUTTON_OK);
            }
        }
        /// <summary>
        /// 执行带有指定特性的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="allMethodInfos"></param>
        private async Task ExcuteMethodInfoByAttributeAsync<T>(IEnumerable<MethodInfo> allMethodInfos)
            where T : Attribute
        {
            foreach (MethodInfo methodInfo in allMethodInfos)
            {
                if (methodInfo.GetCustomAttribute<T>() is null) continue;
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                if (parameterInfos.Length != 0) continue;
                object result = methodInfo.Invoke(this, []);
                if (result is not Task taskResult) break;
                await taskResult;
            }
        }
        /// <summary>
        /// 创建操作模型属性
        /// </summary>
        /// <param name="codeContent"></param>
        /// <param name="property"></param>
        private void GeneratorOperationalModelProperty(StringBuilder codeContent, PropertyModel property)
        {
            if (property.Annotation is not null && !string.IsNullOrWhiteSpace(property.Annotation))
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
            }
            string? verificationAttributesCode = property.GetVerificationAttributesCode();
            if (verificationAttributesCode is not null && !string.IsNullOrWhiteSpace(verificationAttributesCode))
            {
                codeContent.AppendLine($"        {verificationAttributesCode}");
            }
            if (property.HasAttribute<LoginUserIDAttribute>())
            {
                codeContent.AppendLine($"        [{nameof(LoginUserIDAttribute).RemoveAttributeSuffix()}]");
            }
            codeContent.AppendLine($"        public {property.PredefinedType} {property.Name} {{ get; set; }}");
            if (property.Initializer is not null && !string.IsNullOrWhiteSpace(property.Initializer))
            {
                codeContent.Insert(codeContent.Length - 2, $" = {property.Initializer};");
            }
        }
    }
}
