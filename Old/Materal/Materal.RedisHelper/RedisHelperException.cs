using System;
using Materal.Common;

namespace Materal.RedisHelper
{
    /// <inheritdoc />
    /// <summary>
    /// Materal异常类
    /// </summary>
    public class RedisHelperException : MateralException
    {
        public RedisHelperException()
        {
        }

        public RedisHelperException(string message) : base(message)
        {
        }

        public RedisHelperException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
