﻿using Microsoft.Extensions.DependencyInjection.Extensions;
using RC.Authority.Abstractions.HttpClient;

namespace RC.Authority.Application
{
    /// <summary>
    /// Authority模块
    /// </summary>
    public class AuthorityModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.TryAddSingleton<IUserController, UserControllerAccessor>();
            await base.OnConfigServiceAsync(context);
        }
    }
}