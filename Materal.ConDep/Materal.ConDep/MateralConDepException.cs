using Materal.Common;
using System;

namespace Materal.ConDep
{
    public class MateralConDepException : MateralException
    {
        public MateralConDepException()
        {
        }

        public MateralConDepException(string message) : base(message)
        {
        }

        public MateralConDepException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
