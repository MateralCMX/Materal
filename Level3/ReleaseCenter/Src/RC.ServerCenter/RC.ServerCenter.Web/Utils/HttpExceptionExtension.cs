using AntDesign;
using Materal.Abstractions;
using Materal.Utils.Http;
using System.Net;

namespace RC.ServerCenter.Web
{
    public static class HttpExceptionExtension
    {
        public static void HandlerHttpError(this MateralHttpException exception, IMessageService? message = null, NotificationService? notificationService = null)
        {
            message ??= MateralServices.GetService<IMessageService>();
            notificationService ??= MateralServices.GetService<NotificationService>();
            CustomAuthenticationStateProvider authenticationState = MateralServices.GetService<CustomAuthenticationStateProvider>();
            if (exception.HttpRequestMessage == null && exception.HttpResponseMessage == null)
            {
                message.ShowError(exception.Message);
            }
            else if (exception.HttpResponseMessage != null)
            {
                switch (exception.HttpResponseMessage.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        authenticationState.LoginOut();
                        break;
                    default:
                        message.ShowError(exception.Message);
                        break;
                }
            }
            else
            {
                string errorMessage = exception.GetErrorMessage();
                notificationService.ShowErrorMessage(errorMessage);
            }
        }
    }
}
