﻿namespace Materal.Utils.Windows
{
    /// <summary>
    /// 进程管理器
    /// </summary>
    public class ProcessHelper
    {
        /// <summary>
        /// 输出数据
        /// </summary>
        public event DataReceivedEventHandler? OutputDataReceived;
        /// <summary>
        /// 错误数据
        /// </summary>
        public event DataReceivedEventHandler? ErrorDataReceived;
        /// <summary>
        /// 获得进程开始信息
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static ProcessStartInfo GetProcessStartInfo(string cmd, string arg)
        {
            ProcessStartInfo processStartInfo = new()
            {
                FileName = cmd,
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                Verb = "RunAs",
                Arguments = arg
            };
            return processStartInfo;
        }
        /// <summary>
        /// 启动一个进程
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="arg"></param>
        public void ProcessStart(string cmd, string arg)
        {
            ProcessStartInfo processStartInfo = GetProcessStartInfo(cmd, arg);
            using Process process = new() { StartInfo = processStartInfo };
            if (OutputDataReceived is not null)
            {
                process.OutputDataReceived += OutputDataReceived;
            }
            if (ErrorDataReceived is not null)
            {
                process.ErrorDataReceived += ErrorDataReceived;
            }
            if (process.Start())
            {
                if (OutputDataReceived is not null)
                {
                    process.BeginOutputReadLine();
                }
                if (ErrorDataReceived is not null)
                {
                    process.BeginErrorReadLine();
                }
            }
            process.StandardInput.AutoFlush = true;
            process.WaitForExit();
            process.Close();
        }
    }
}
