using System;
using Materal.Common;

namespace Materal.APP.Core.Models
{
    public class MateralAPPException : MateralException
    {
        public MateralAPPException()
        {
        }

        public MateralAPPException(string message)
            : base(message)
        {
        }

        public MateralAPPException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
