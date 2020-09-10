using Materal.APP.Core;
using System;

namespace ConfigCenter.Common
{
    public class ConfigCenterException : MateralAPPException
    {
        public ConfigCenterException() { }

        public ConfigCenterException(string message) : base(message) { }

        public ConfigCenterException(string message, Exception innerException) : base(message, innerException) { }
    }
}
