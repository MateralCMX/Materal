using Ocelot.Errors;
using System.Net;

namespace Materal.Gateway.OcelotExtension.Custom
{
    public class CustomError : Error
    {
        public CustomError(string message, HttpStatusCode httpStatusCode) : base(message, OcelotErrorCode.UnknownError, (int)httpStatusCode)
        {
        }
    }
}
