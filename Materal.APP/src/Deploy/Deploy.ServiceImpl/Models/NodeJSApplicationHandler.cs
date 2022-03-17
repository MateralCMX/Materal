using Deploy.Common;
using Deploy.Enums;
using Materal.StringHelper;
using Materal.WindowsHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Deploy.ServiceImpl.Models
{
    public class NodeJSApplicationHandler : ApplicationHandler
    {
        private const string NodePathCommand = "npm config get prefix";
        private string _nodePath;
        public override void StartApplication(ApplicationRuntimeModel applicationRuntime)
        {
            if (applicationRuntime.Status != ApplicationStatusEnum.Stop) throw new DeployException("应用程序尚未停止");
            applicationRuntime.Status = ApplicationStatusEnum.ReadyRun;
            if (HasHazardParams(applicationRuntime.Path)) throw new DeployException("参数包含危险参数");
            if (HasHazardParams(applicationRuntime.StopParams)) throw new DeployException("参数包含危险参数");
            try
            {
                ProcessStartInfo processStartInfo = ProcessManager.GetProcessStartInfo("cmd.exe", string.Empty);
                BindProcess = new Process { StartInfo = processStartInfo };
                ConsoleMessage = new List<string>();
                bool isGetNodePathCommand = false;
                void DataHandler(object sender, DataReceivedEventArgs e)
                {
                    if (string.IsNullOrWhiteSpace(e.Data)) return;
                    ConsoleMessage.Add(e.Data);
                    Console.WriteLine(e.Data);
                    if (isGetNodePathCommand && e.Data.IsAbsoluteDirectoryPath())
                    {
                        isGetNodePathCommand = false;
                        _nodePath = $"{e.Data}\\node_modules";
                        ConsoleMessage.Add($"已获取NodePath为{_nodePath}");
                        Console.WriteLine(e.Data);
                        string startArgs = GetStartArgs(applicationRuntime, _nodePath);
                        var trueMainModule = ReplaceCommand(applicationRuntime.MainModule, applicationRuntime);
                        if (HasHazardParams(trueMainModule))
                        {
                            BindProcess.StandardInput.WriteLine($"启动失败,包含危险参数");
                            StopApplication(applicationRuntime);
                            return;
                        }
                        else
                        {
                            BindProcess.StandardInput.WriteLine($"{trueMainModule} install && node.exe {startArgs}");
                        }
                    }
                    else
                    {
                        isGetNodePathCommand = e.Data.EndsWith(NodePathCommand);
                    }
                }
                BindProcess.OutputDataReceived += DataHandler;
                BindProcess.ErrorDataReceived += DataHandler;
                if (BindProcess.Start())
                {
                    BindProcess.BeginOutputReadLine();
                    BindProcess.BeginErrorReadLine();
                    IEnumerable<string> commands = GetCommands(applicationRuntime);
                    foreach (string command in commands)
                    {
                        BindProcess.StandardInput.WriteLine(command);
                    }
                }
                else
                {
                    throw new DeployException("启动失败");
                }
                applicationRuntime.Status = ApplicationStatusEnum.Running;
                ClearConsoleMessageTimer.Start();
            }
            catch (Exception)
            {
                applicationRuntime.Status = ApplicationStatusEnum.Stop;
                throw;
            }
        }

        public override void StopApplication(ApplicationRuntimeModel applicationRuntime)
        {
            StopApplication(applicationRuntime, ApplicationTypeEnum.NodeJS);
        }
        protected override void CloseProcess(ApplicationRuntimeModel applicationRuntime)
        {
            if (BindProcess == null) return;
            IEnumerable<string> commands = GetStopCommands(applicationRuntime);
            foreach (string command in commands)
            {
                BindProcess.StandardInput.WriteLine(command);
            }
            BindProcess.WaitForExit();
            BindProcess.Dispose();
        }
        #region 私有方法
        /// <summary>
        /// 获得启动参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private IEnumerable<string> GetStopCommands(ApplicationRuntimeModel model)
        {
            var stopParam = ReplaceCommand(model.StopParams, model);
            var result = new List<string>
            {
                $"node.exe {stopParam}",
                "Exit"
            };
            return result;
        }
        /// <summary>
        /// 替换命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private string ReplaceCommand(string command, ApplicationRuntimeModel model)
        {
            var result = command.Replace("{$Path}", model.Path);
            result = result.Replace("{$NodePath}", _nodePath);
            return result;
        }
        /// <summary>
        /// 获得启动参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private IEnumerable<string> GetCommands(ApplicationRuntimeModel model)
        {
            var result = new List<string>
            {
                $"cd \"Application\\{model.Path}\"",
                NodePathCommand
            };
            return result;
        }
        /// <summary>
        /// 获得启动参数
        /// </summary>
        /// <param name="model"></param>
        /// <param name="nodePath"></param>
        /// <returns></returns>
        private string GetStartArgs(ApplicationRuntimeModel model, string nodePath)
        {
            string result = ReplaceCommand(model.RunParams, model);
            if (HasHazardParams(result)) throw new DeployException("参数包含危险参数");
            return result;
        }
        /// <summary>
        /// 有危险参数
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool HasHazardParams(string command)
        {
            Func<string, bool>[] funcs = {
                m => m.Contains("&"),
                m => m.Contains("|"),
            };
            return funcs.Any(func => func.Invoke(command));
        }
        #endregion
    }
}
