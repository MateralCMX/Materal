using AntDesign;
using Materal.Abstractions;
using Materal.Utils.Http;

namespace RC.ServerCenter.Web
{
    public static class HttpHandler
    {
        public static void Handler(Func<Task> httpHandler, Action? afterHandler = null, Action? successHandler = null, Action? errorHandler = null)
        {
            IMessageService message = MateralServices.GetService<IMessageService>();
            NotificationService notificationService = MateralServices.GetService<NotificationService>();
            Task.Run(async () =>
            {
                try
                {
                    await httpHandler();
                    successHandler?.Invoke();
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
