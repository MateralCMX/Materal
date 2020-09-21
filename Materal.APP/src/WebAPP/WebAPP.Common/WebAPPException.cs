using System;
using Materal.APP.Core;

namespace WebAPP.Common
{
    public class WebAPPException : MateralAPPException
    {
        public WebAPPException()
        {
        }

        public WebAPPException(string message) : base(message)
        {
        }

        public WebAPPException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
