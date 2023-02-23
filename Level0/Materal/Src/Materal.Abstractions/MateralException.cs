namespace Materal.Abstractions
{
    /// <summary>
    /// Materal基础异常类
    /// </summary>
    public class MateralException : Exception
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MateralException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
