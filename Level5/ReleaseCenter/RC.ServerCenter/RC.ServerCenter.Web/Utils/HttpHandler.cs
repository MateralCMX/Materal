using AntDesign;
using Materal.Abstractions;
using Materal.Utils.Http;

namespace RC.ServerCenter.Web
{
    public static class HttpHandler
    {
        public static void Handler(Func<Task> httpHandler, Action? afterHandler = null, Action? errorHandler = null)
        {
            IMessageService message = MateralServices.GetRequiredService<IMessageService>();
            NotificationService notificationService = MateralServices.GetRequiredService<NotificationService>();
            Task.Run(async () =>
            {
                try
                {
                    await httpHandler();
                }
                catch (MateralHttpException ex)
                {
                    errorHandler?.Invoke();
                    ex.HandlerHttpError(message, notificationService);
                }
                catch (Exception ex)
                {
                    errorHandler?.Invoke();
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
