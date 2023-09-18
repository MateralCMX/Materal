﻿//using Materal.Logger.Models;

//namespace Materal.Logger.LoggerHandlers.Models
//{
//    /// <summary>
//    /// 文件日志处理器模型
//    /// </summary>
//    public class FileLoggerHandlerModel : BufferLoggerHandlerModel
//    {
//        /// <summary>
//        /// 输出的消息
//        /// </summary>
//        public string FileContent { get; } = string.Empty;
//        /// <summary>
//        /// 路径
//        /// </summary>
//        public string Path { get; } = string.Empty;
//        /// <summary>
//        /// 颜色
//        /// </summary>
//        public ConsoleColor Color { get; }
//        /// <summary>
//        /// 构造方法
//        /// </summary>
//        /// <param name="rule"></param>
//        /// <param name="target"></param>
//        /// <param name="model"></param>
//        public FileLoggerHandlerModel(LoggerRuleConfigModel rule, FileLoggerTargetConfigModel target, LoggerHandlerModel model) : base(rule, target, model)
//        {
//            Path = LoggerHandlerHelper.FormatPath(target.Path, model.LogLevel, model.CategoryName, model.Scope, model.CreateTime, model.ThreadID);
//            FileContent = LoggerHandlerHelper.FormatMessage(target.Format, model.LogLevel, model.Message, model.CategoryName, model.Scope, model.CreateTime, model.Exception, model.ThreadID, model.ID);
//        }
//    }
//}
