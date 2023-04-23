using AntDesign;

namespace RC.ServerCenter.Web
{
    public static class NotificationServiceExtension
    {
        public static void ShowInfoMessage(this NotificationService notificationService, string message) => notificationService.ShowInfo(new()
        {
            Message = "消息",
            Description = message
        });
        public static void ShowSuccessMessage(this NotificationService notificationService, string message) => notificationService.ShowSuccess(new()
        {
            Message = "成功",
            Description = message
        });
        public static void ShowWarningMessage(this NotificationService notificationService, string message) => notificationService.ShowWarning(new()
        {
            Message = "警告",
            Description = message
        });
        public static void ShowErrorMessage(this NotificationService notificationService, string message) => notificationService.ShowError(new()
        {
            Message = "错误",
            Description = message
        });
        public static void ShowInfo(this NotificationService notificationService, NotificationConfig config) => notificationService.Info(config);
        public static void ShowSuccess(this NotificationService notificationService, NotificationConfig config) => notificationService.Success(config);
        public static void ShowWarning(this NotificationService notificationService, NotificationConfig config) => notificationService.Warning(config);
        public static void ShowError(this NotificationService notificationService, NotificationConfig config) => notificationService.Error(config);
    }
}
