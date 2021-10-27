using System;
using Materal.APP.Core;

namespace ConfigCenter.Environment.Common
{
    public class ConfigCenterEnvironmentException : MateralAPPException
    {
        public ConfigCenterEnvironmentException() { }

        public ConfigCenterEnvironmentException(string message) : base(message) { }

        public ConfigCenterEnvironmentException(string message, Exception innerException) : base(message, innerException) { }
    }
}
