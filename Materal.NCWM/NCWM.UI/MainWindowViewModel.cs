﻿using System.Deployment.Application;
using System.Reflection;

namespace NCWM.UI
{
    public class MainWindowViewModel
    {
        private string _title = "Materal.NetCore启动器";
        /// <summary>
        /// 运行状态
        /// </summary>
        public bool IsRun { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get => _title + " 当前版本：" + Version;
            set => _title = value;
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get
            {
                string result = "版本号：";
                try
                {
                    result += ApplicationDeployment.CurrentDeployment.CurrentVersion;
                }
                catch
                {
                    result += Assembly.GetExecutingAssembly().GetName().Version.ToString();
                }
                return result;
            }
        }
    }
}
