using Materal.Common;
using System;

namespace Materal.StringHelper
{
    public class MateralStringHelperException : MateralException
    {
        public MateralStringHelperException()
        {
        }

        public MateralStringHelperException(string message) : base(message)
        {
        }

        public MateralStringHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
