using System;

namespace Materal.WebSocket.Model
{
    public class MateralWebSocketClientException : MateralWebSocketException
    {
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralWebSocketClientException()
        {
        }
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public MateralWebSocketClientException(string message) : base(message)
        {
        }
        /// <inheritdoc />
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">
        ///     The exception that is the cause of the current exception. If the innerException
        ///     parameter is not a null reference, the current exception is raised in a catch
        ///     block that handles the inner exception.
        /// </param>
        public MateralWebSocketClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
