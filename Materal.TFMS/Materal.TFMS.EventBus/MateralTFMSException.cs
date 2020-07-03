using System;
using System.Collections.Generic;
using Materal.Common;

namespace Materal.TFMS.EventBus
{
    public class MateralTFMSException : MateralException
    {
        public MateralTFMSException() { }
        public MateralTFMSException(string message) : base(message) { }
        public MateralTFMSException(string message, Exception innerException) : base(message, innerException) { }
    }
}
