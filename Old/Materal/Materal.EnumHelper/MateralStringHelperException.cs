using Materal.Common;
using System;

namespace Materal.EnumHelper
{
    public class MateralEnumHelperException : MateralException
    {
        public MateralEnumHelperException()
        {
        }

        public MateralEnumHelperException(string message) : base(message)
        {
        }

        public MateralEnumHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
