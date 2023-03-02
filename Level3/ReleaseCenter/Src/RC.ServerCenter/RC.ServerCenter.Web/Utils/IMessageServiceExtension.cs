using AntDesign;

namespace RC.ServerCenter.Web
{
    public static class IMessageServiceExtension
    {
        public static void ShowInfo(this IMessageService messageService, string message) => messageService.Info(message);
        public static void ShowSuccess(this IMessageService messageService, string message) => messageService.Success(message);
        public static void ShowWarning(this IMessageService messageService, string message) => messageService.Warning(message);
        public static void ShowError(this IMessageService messageService, string message) => messageService.Error(message);
    }
}
