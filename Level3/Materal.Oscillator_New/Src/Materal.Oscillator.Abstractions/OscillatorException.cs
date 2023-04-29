using Materal.Abstractions;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// Oscillator异常类
    /// </summary>
    public class OscillatorException : MateralException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public OscillatorException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public OscillatorException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public OscillatorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
