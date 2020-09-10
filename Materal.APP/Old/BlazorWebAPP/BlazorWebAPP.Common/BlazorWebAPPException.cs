using Materal.APP.Common;
using System;

namespace BlazorWebAPP.Common
{
    public class BlazorWebAPPException : MateralAPPException
    {
        public BlazorWebAPPException()
        {
        }

        public BlazorWebAPPException(string message) : base(message)
        {
        }

        public BlazorWebAPPException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
