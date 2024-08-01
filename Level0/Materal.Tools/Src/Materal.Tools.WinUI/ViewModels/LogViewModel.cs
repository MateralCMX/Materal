using CommunityToolkit.Mvvm.ComponentModel;
using Materal.Tools.Core.Logger;
using Microsoft.Extensions.Logging;
using System;

namespace Materal.Tools.WinUI.ViewModels
{
    public partial class LogViewModel(Log log) : ObservableObject
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [ObservableProperty]
        private Guid _ID = log.ID;
        /// <summary>
        /// 创建时间
        /// </summary>
        [ObservableProperty]
        private DateTime _createTime = log.CreateTime;
        /// <summary>
        /// 日志等级
        /// </summary>
        [ObservableProperty]
        private LogLevel _level = log.Level;
        /// <summary>
        /// 事件ID
        /// </summary>
        [ObservableProperty]
        private EventId _eventID = log.EventID;
        /// <summary>
        /// 分类名称
        /// </summary>
        [ObservableProperty]
        private string _categoryName = log.CategoryName;
        /// <summary>
        /// 状态
        /// </summary>
        [ObservableProperty]
        private string _message = log.Message;
        /// <summary>
        /// 异常
        /// </summary>
        [ObservableProperty]
        private Exception? _exception = log.Exception;
        public string CompositeMessage => Exception is null ?
            $"[{CreateTime:yyyy/MM/dd HH:mm:ss}|{Level}]{Message}" :
            $"[{CreateTime:yyyy/MM/dd HH:mm:ss}|{Level}]{Message}\r\n{Exception.Message}\r\n{Exception.StackTrace}";
    }
}
