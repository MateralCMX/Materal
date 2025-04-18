﻿using Materal.MergeBlock;

namespace MMB.NewModule.WebAPI
{
    /// <summary>
    /// 主程序
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 入口
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args) => await MergeBlockProgram.RunAsync(args);
    }
}