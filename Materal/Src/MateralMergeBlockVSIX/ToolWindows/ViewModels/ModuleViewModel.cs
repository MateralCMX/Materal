using Materal.Utils.Windows;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell.Interop;
using System.IO;
using System.Linq;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public class ModuleViewModel : ObservableObject
    {
        private string _solutionName = string.Empty;
        private string _moduleName = string.Empty;
        private string _applicationCsprojPath = string.Empty;
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get => _moduleName; }
        private string _solutionPath = string.Empty;
        /// <summary>
        /// 解决方案路径
        /// </summary>
        public string SolutionPath
        {
            get => _solutionPath;
            set
            {
                _solutionPath = value;
                _solutionName = Path.GetFileNameWithoutExtension(value);
                string[] solutionNames = _solutionName.Split('.');
                string projectName = solutionNames.First();
                _moduleName = solutionNames.Last();
                DirectoryInfo directoryInfo = new FileInfo(_solutionPath).Directory;
                if (_moduleName == "Core")
                {
                    _applicationCsprojPath = Path.Combine(directoryInfo.FullName, "Demo", $"{projectName}.Demo.Application", $"{projectName}.Demo.Application.csproj");
                }
                else
                {
                    _applicationCsprojPath = Path.Combine(directoryInfo.FullName, $"{_solutionName}.Application", $"{_solutionName}.Application.csproj");
                }
                NotifyPropertyChanged();
            }
        }
        private bool _canOpen = false;
        /// <summary>
        /// 能否打开
        /// </summary>
        public bool CanOpen { get => _canOpen; set { _canOpen = value; NotifyPropertyChanged(); } }
        /// <summary>
        /// 打开Sln文件
        /// </summary>
        public void Open()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (!File.Exists(SolutionPath)) return;
            if (Package.GetGlobalService(typeof(SVsSolution)) is not IVsSolution solution) return;
            solution.OpenSolutionFile((uint)__VSSLNOPENOPTIONS.SLNOPENOPT_Silent, SolutionPath);
        }
        /// <summary>
        /// 构建
        /// </summary>
        public async Task BuildAsync()
        {
            CmdHelper cmdHelper = new();
            string[] cmds = [$"dotnet build {_applicationCsprojPath} -c Debug"];
            await cmdHelper.RunCmdCommandsAsync(cmds);
        }
    }
}
