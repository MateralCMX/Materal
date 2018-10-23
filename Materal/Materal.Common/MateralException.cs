using System;
using System.Runtime.Serialization;

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

        protected MateralException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
