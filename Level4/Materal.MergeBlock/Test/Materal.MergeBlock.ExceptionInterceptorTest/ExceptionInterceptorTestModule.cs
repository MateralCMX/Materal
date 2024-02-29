﻿using Materal.Extensions;
using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.Extensions.Configuration;

[assembly: MergeBlockAssembly(true)]
namespace Materal.MergeBlock.ExceptionInterceptorTest
{
    /// <summary>
    /// ExceptionInterceptor模块
    /// </summary>
    public class ExceptionInterceptorTestModule : MergeBlockWebModule, IMergeBlockWebModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ExceptionInterceptorTestModule() : base("ExceptionInterceptor测试模块", "ExceptionInterceptorTest", ["ExceptionInterceptor"])
        {

        }
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            string configFilePath = Path.Combine(GetType().Assembly.GetDirectoryPath(), "ExceptionInterceptorConfig.json");
            if (File.Exists(configFilePath))
            {
                context.Configuration.AddJsonFile(configFilePath, true, true);
            }
            await base.OnConfigServiceAsync(context);
        }
    }
}