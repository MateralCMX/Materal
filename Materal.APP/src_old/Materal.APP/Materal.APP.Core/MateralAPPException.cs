using Materal.Common;
using System;

namespace Materal.APP.Core
{
    public class MateralAPPException : MateralException
    {
        public MateralAPPException() { }
        public MateralAPPException(string message) : base(message) { }
        public MateralAPPException(string message, Exception innerException) : base(message, innerException) { }
    }
}
