﻿namespace Materal.MergeBlock.ConsoleTest
{
    /// <summary>
    /// 入口程序
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 入口方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args) => await Materal.MergeBlock.ConsoleHosting.MergeBlockProgram.RunAsync(args);
    }
}
