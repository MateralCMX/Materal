using Materal.Common;
using System;

namespace Materal.DBHelper
{
    public class MateralDBHelperException : MateralException
    {
        public MateralDBHelperException()
        {
        }

        public MateralDBHelperException(string message) : base(message)
        {
        }

        public MateralDBHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
