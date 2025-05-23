﻿using RC.Deploy.Application.Services.Models;
using System.Collections.Concurrent;

namespace RC.Deploy.Application.Services
{
    /// <summary>
    /// 应用程序运行主机
    /// </summary>
    public class ApplicationRuntimeHost
    {
        /// <summary>
        /// 应用程序运行对象字典
        /// </summary>
        public static ConcurrentDictionary<Guid, ApplicationRuntimeModel> ApplicationRuntimes { get; } = new();
        /// <summary>
        /// 关闭
        /// </summary>
        public static async Task ShutDownAsync()
        {
            foreach (KeyValuePair<Guid, ApplicationRuntimeModel> item in ApplicationRuntimes)
            {
                if (item.Value.ApplicationStatus != ApplicationStatusEnum.Runing) continue;
                await item.Value.ShutDownAsync();
            }
        }
    }
}
