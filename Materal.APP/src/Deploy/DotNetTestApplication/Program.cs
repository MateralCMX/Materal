using System;
using System.Collections.Generic;
using System.Timers;
using Deploy.Enums;
using Deploy.ServiceImpl.Models;

namespace DotNetTestApplication
{
    public class Program
    {
        private static ApplicationRuntimeModel _nodeJsApplicationRuntime;
        public static void Main(string[] args)
        {
            //Console.WriteLine("DotNetTestApplication");
            //foreach (string arg in args)
            //{
            //    Console.WriteLine(arg);
            //}
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            _nodeJsApplicationRuntime = new ApplicationRuntimeModel
            {
                ID = Guid.NewGuid(),
                MainModule = "npm --registry http://116.55.251.31:4873",
                Name = "测试NodeJS",
                OtherParams = string.Empty,
                Path = "DeployNuxtDemo",
                RunParams = "\"node_modules/cross-env/src/bin/cross-env.js\" Test=qwer \"{$NodePath}\\pm2\\bin\\pm2\" start",
                StopParams = "\"{$NodePath}\\pm2\\bin\\pm2\" delete {$Path}",
                ApplicationType = ApplicationTypeEnum.NodeJS,
                Status = ApplicationStatusEnum.Stop
            };
            _nodeJsApplicationRuntime.Start();
            Console.ReadKey();
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            _nodeJsApplicationRuntime?.Stop();
        }
    }
}
