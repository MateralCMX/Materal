using System;

namespace Materal.Common
{
    /// <inheritdoc />
    /// <summary>
    /// Materal异常类
    /// </summary>
    public class MateralException : Exception
    {
        public MateralException()
        {
        }

        public MateralException(string message) : base(message)
        {
        }

        public MateralException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
