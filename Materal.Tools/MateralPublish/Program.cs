﻿using MateralPublish.Models;
using System.CommandLine;

namespace MateralPublish
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Option<string?> canAllOption = new("--Version", "指定版本号");
            canAllOption.AddAlias("-v");
            canAllOption.IsRequired = false;
            canAllOption.SetDefaultValue(null);
            RootCommand rootCommand = new("发布Materal项目");
            rootCommand.AddOption(canAllOption);
            rootCommand.SetHandler(Publish, canAllOption);
            return await rootCommand.InvokeAsync(args);
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="canAll"></param>
        private static void Publish(string? newVersion)
        {
            MateralProjectModel materalProject = new(AppDomain.CurrentDomain.BaseDirectory);
            materalProject.Publish(newVersion);
        }
    }
}