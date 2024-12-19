using CommunityToolkit.Mvvm.ComponentModel;
using Materal.Tools.Core.Logger;
using Microsoft.Extensions.Logging;

namespace Materal.Tools.WinUI.ViewModels
{
    public partial class LogViewModel(Log log) : ObservableObject
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [ObservableProperty]
        public partial Guid ID { get; set; } = log.ID;
        /// <summary>
        /// 创建时间
        /// </summary>
        [ObservableProperty]
        public partial DateTime CreateTime { get; set; } = log.CreateTime;
        /// <summary>
        /// 日志等级
        /// </summary>
        [ObservableProperty]
        public partial LogLevel Level { get; set; } = log.Level;
        /// <summary>
        /// 事件ID
        /// </summary>
        [ObservableProperty]
        public partial EventId EventID { get; set; } = log.EventID;
        /// <summary>
        /// 分类名称
        /// </summary>
        [ObservableProperty]
        public partial string CategoryName { get; set; } = log.CategoryName;
        /// <summary>
        /// 状态
        /// </summary>
        [ObservableProperty]
        public partial string Message { get; set; } = log.Message;
        /// <summary>
        /// 异常
        /// </summary>
        [ObservableProperty]
        public partial Exception? Exception { get; set; } = log.Exception;
        public string CompositeMessage => Exception is null ?
            $"[{CreateTime:yyyy/MM/dd HH:mm:ss}|{Level}]{Message}" :
            $"[{CreateTime:yyyy/MM/dd HH:mm:ss}|{Level}]{Message}\r\n{Exception.Message}\r\n{Exception.StackTrace}";
    }
}
