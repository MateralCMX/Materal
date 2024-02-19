#nullable enable
using Materal.Abstractions;
using Materal.MergeBlock.GeneratorCode.Extensions;
using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using Materal.MergeBlock.GeneratorCode;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedViewModel : ObservableObject
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
        private SolutionItem? _coreRepository;
        private SolutionItem? _moduleAbstractions;
        private SolutionItem? _moduleApplication;
        private SolutionItem? _moduleRepository;
        private SolutionItem? _moduleWebAPI;
        private readonly List<Type> _generatorCodePlugTypes = [];
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="solution"></param>
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
                _coreRepository = null;
                _moduleAbstractions = null;
                _moduleApplication = null;
                _moduleRepository = null;
                _moduleWebAPI = null;
            }
        }
        /// <summary>
        /// 绑定解决方案项
        /// </summary>
        /// <param name="solutionItems"></param>
        private void BindingSolutionItems(IEnumerable<SolutionItem?> solutionItems)
        {
            foreach (SolutionItem? solutionItem in solutionItems)
            {
                if (solutionItem is null) continue;
                BindingSolutionItem(solutionItem);
            }
        }
        /// <summary>
        /// 绑定解决方案项
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <exception cref="Exception"></exception>
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
                else if (_projectName != projectNames[0]) return;
                if (projectNames[1] != "Core")
                {
                    if (_moduleName is null)
                    {
                        _moduleName = projectNames[1];
                    }
                    else if (_moduleName != projectNames[1]) return;
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
                            _moduleRepository = solutionItem;
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
                            _coreRepository = solutionItem;
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 生成代码
        /// </summary>
        public async Task GeneratorCodeAsync()
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
                GeneratorCodeContext context = new(coreAbstractionsPath, coreRepositoryPath, moduleAbstractionsPath, moduleApplicationPath, moduleRepositoryPath, moduleWebAPIPath);
                context.Refresh(_moduleAbstractions);
                ExcuteMethodInfoByAttribute<GeneratorCodeBeforMethodAttribute>(allMethodInfos, context);
                context.Refresh(_moduleAbstractions);
                ExcuteMethodInfoByAttribute<GeneratorCodeMethodAttribute>(allMethodInfos, context);
                context.Refresh(_moduleAbstractions);
                ExcuteMethodInfoByAttribute<GeneratorCodeAfterMethodAttribute>(allMethodInfos, context);
                context.Refresh(_moduleAbstractions);
                List<string> generatorCodePlugPaths = [];
                generatorCodePlugPaths.AddRange(_coreAbstractions?.GetGeneratorCodePlugPaths() ?? []);
                generatorCodePlugPaths.AddRange(_coreRepository?.GetGeneratorCodePlugPaths() ?? []);
                generatorCodePlugPaths.AddRange(_moduleAbstractions?.GetGeneratorCodePlugPaths() ?? []);
                generatorCodePlugPaths.AddRange(_moduleRepository?.GetGeneratorCodePlugPaths() ?? []);
                generatorCodePlugPaths.AddRange(_moduleApplication?.GetGeneratorCodePlugPaths() ?? []);
                generatorCodePlugPaths.AddRange(_moduleWebAPI?.GetGeneratorCodePlugPaths() ?? []);
                if(generatorCodePlugPaths.Count > 0)
                {
                    await ExcutePlugAsync(generatorCodePlugPaths, context);
                }
                await VS.MessageBox.ShowAsync("提示", "代码生成完毕", Microsoft.VisualStudio.Shell.Interop.OLEMSGICON.OLEMSGICON_INFO, Microsoft.VisualStudio.Shell.Interop.OLEMSGBUTTON.OLEMSGBUTTON_OK);
            }
            catch (Exception ex)
            {
                await VS.MessageBox.ShowAsync("错误", ex.GetErrorMessage(), Microsoft.VisualStudio.Shell.Interop.OLEMSGICON.OLEMSGICON_WARNING, Microsoft.VisualStudio.Shell.Interop.OLEMSGBUTTON.OLEMSGBUTTON_OK);
            }
        }
        /// <summary>
        /// 执行插件
        /// </summary>
        /// <param name="codePaths"></param>
        /// <param name="context"></param>
        private async Task ExcutePlugAsync(IReadOnlyCollection<string> codePaths, GeneratorCodeContext context)
        {
            foreach (string codePath in codePaths)
            {
                try
                {
                    IMergeBlockGeneratorCodePlug plug = await GetMergeBlockGeneratorCodePlugAsync(codePath);
                    await plug.ExcuteAsync(context);
                }
                catch (Exception ex)
                {
                    await VS.MessageBox.ShowAsync($"插件[{codePath}]错误", ex.GetErrorMessage(), Microsoft.VisualStudio.Shell.Interop.OLEMSGICON.OLEMSGICON_WARNING, Microsoft.VisualStudio.Shell.Interop.OLEMSGBUTTON.OLEMSGBUTTON_OK);
                }
            }
        }
        /// <summary>
        /// 执行带有指定特性的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="allMethodInfos"></param>
        /// <param name="context"></param>
        private void ExcuteMethodInfoByAttribute<T>(IEnumerable<MethodInfo> allMethodInfos, GeneratorCodeContext context)
            where T : Attribute
        {
            foreach (MethodInfo methodInfo in allMethodInfos)
            {
                if (methodInfo.GetCustomAttribute<T>() is null) continue;
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                if (parameterInfos.Length == 0)
                {
                    methodInfo.Invoke(this, []);
                }
                else if (parameterInfos.Length == 1)
                {
                    ParameterInfo parameterInfo = parameterInfos.First();
                    if (parameterInfo.ParameterType == typeof(List<DomainModel>))
                    {
                        methodInfo.Invoke(this, [context.Domains]);
                    }
                    else if (parameterInfo.ParameterType == typeof(List<IServiceModel>))
                    {
                        methodInfo.Invoke(this, [context.Services]);
                    }
                    else if (parameterInfo.ParameterType == typeof(List<IControllerModel>))
                    {
                        methodInfo.Invoke(this, [context.Controllers]);
                    }
                    else if (parameterInfo.ParameterType == typeof(List<EnumModel>))
                    {
                        methodInfo.Invoke(this, [context.Enums]);
                    }
                }
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
                codeContent.Insert(codeContent.Length - 2, $"  = {property.Initializer};");
            }
        }
        /// <summary>
        /// 获得MergeBlock生成代码插件
        /// </summary>
        /// <param name="codeFilePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<IMergeBlockGeneratorCodePlug> GetMergeBlockGeneratorCodePlugAsync(string codeFilePath)
        {
            if (!codeFilePath.EndsWith(".cs")) throw new Exception("不是C#文件");
            string codeFileName = Path.GetFileNameWithoutExtension(codeFilePath);
            string scriptCode = File.ReadAllText(codeFilePath);
            StringBuilder codeContent = new();
            codeContent.AppendLine("using System;");
            codeContent.AppendLine("using System.IO;");
            codeContent.AppendLine("using System.Text;");
            codeContent.AppendLine("using System.Threading.Tasks;");
            codeContent.AppendLine(scriptCode);
            scriptCode = codeContent.ToString();
            Assembly[] trueAssemblies =
            [
                Assembly.Load("Materal.MergeBlock.GeneratorCode"),
            ];
            ScriptOptions scriptOptions = ScriptOptions.Default
                .WithReferences(trueAssemblies);
            Script<IMergeBlockGeneratorCodePlug> script = CSharpScript.Create<IMergeBlockGeneratorCodePlug>(scriptCode, scriptOptions);
            script.Compile();
            Script<IMergeBlockGeneratorCodePlug> scripteState = script.ContinueWith<IMergeBlockGeneratorCodePlug>($"new {codeFileName}()");
            ScriptState<IMergeBlockGeneratorCodePlug> myInterface = await scripteState.RunAsync();
            IMergeBlockGeneratorCodePlug result = myInterface.ReturnValue;
            return result;
        }
    }
}
