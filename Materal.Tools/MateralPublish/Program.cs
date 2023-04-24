﻿using MateralPublish.Models;
using System.CommandLine;

namespace MateralPublish
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            RootCommand rootCommand = new("发布Materal项目");
            Option<string?> verionOption = new("--Version", "指定版本号");
            verionOption.AddAlias("-v");
            verionOption.IsRequired = false;
            verionOption.SetDefaultValue(null);
            rootCommand.AddOption(verionOption);
            rootCommand.SetHandler(PublishAsync, verionOption);
            return await rootCommand.InvokeAsync(args);
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="canAll"></param>
        private static async Task PublishAsync(string? newVersion)
        {
            MateralProjectModel materalProject = new(AppDomain.CurrentDomain.BaseDirectory);
            string version = newVersion ?? await materalProject.GetNextVersionAsync();
            await materalProject.PublishAsync(version);
        }
    }
}