using Materal.APP.Core;
using System;

namespace Deploy.Common
{
    public class DeployException : MateralAPPException
    {
        public DeployException() { }

        public DeployException(string message) : base(message) { }

        public DeployException(string message, Exception innerException) : base(message, innerException) { }
    }
}
