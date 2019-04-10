using Materal.Common;
using System;

namespace Materal.ConvertHelper
{
    public class MateralConvertException : MateralException
    {
        public MateralConvertException()
        {
        }

        public MateralConvertException(string message) : base(message)
        {
        }

        public MateralConvertException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
