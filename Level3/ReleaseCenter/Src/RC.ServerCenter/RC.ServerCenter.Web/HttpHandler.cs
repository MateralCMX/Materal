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
            Task.Run(async () =>
            {
                try
                {
                    await httpHandler();
                }
                catch (MateralHttpException ex)
                {
                    ex.HandlerHttpError(message);
                }
                catch (Exception ex)
                {
                    await message.Error(ex.Message);
                }
                finally
                {
                    afterHandler?.Invoke();
                }
            });
        }
    }
}
