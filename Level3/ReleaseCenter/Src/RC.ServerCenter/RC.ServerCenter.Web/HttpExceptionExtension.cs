using AntDesign;
using Materal.Abstractions;
using Materal.Utils.Http;
using System.Net;

namespace RC.ServerCenter.Web
{
    public static class HttpExceptionExtension
    {
        public static void HandlerHttpError(this MateralHttpException exception, IMessageService? message = null)
        {
            message ??= MateralServices.GetService<IMessageService>();
            CustomAuthenticationStateProvider authenticationState = MateralServices.GetService<CustomAuthenticationStateProvider>();
            if (exception.HttpResponseMessage != null)
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
                message.ShowError(exception.Message);
            }
        }
    }
}
