﻿using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Common;
using Materal.Oscillator.QuartZExtend;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Reflection;

namespace Materal.Oscillator
{
    public static class OscillatorDIExtension
    {
        /// <summary>
        /// 添加Oscillator服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddOscillatorService(this IServiceCollection services, IConfiguration? configuration = null, params Assembly[] autoMapperAssemblys)
        {
            if(configuration != null)
            {
                OscillatorConfig.Init(configuration);
            }
            List<Assembly> autoMapperAssemblyList = autoMapperAssemblys.ToList();
            autoMapperAssemblyList.Add(Assembly.Load("Materal.Oscillator"));
            services.AddAutoMapper(autoMapperAssemblyList);
            services.AddTransient<IOscillatorManager, OscillatorManager>();
            services.AddTransient<IWorkEventBus, WorkEventBusImpl>();
            services.AddSingleton<OscillatorService>();
            services.AddTransient<IJobListener, JobListener>();
            return services;
        }
    }
}