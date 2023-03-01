using AntDesign;
using Materal.Abstractions;
using Materal.Utils.Http;

namespace RC.ServerCenter.Web
{
    public static class HttpHandler
    {
        public static void Handler(Func<Task> httpHandler, Action? afterHandler = null)
        {
            IMessageService message = MateralServices.GetService<IMessageService>();
            NotificationService notificationService = MateralServices.GetService<NotificationService>();
            Task.Run(async () =>
            {
                try
                {
                    await httpHandler();
                }
                catch (MateralHttpException ex)
                {
                    ex.HandlerHttpError(message, notificationService);
                }
                catch (Exception ex)
                {
                    notificationService.ShowErrorMessage(ex.GetErrorMessage());
                }
                finally
                {
                    afterHandler?.Invoke();
                }
            });
        }
    }
}
