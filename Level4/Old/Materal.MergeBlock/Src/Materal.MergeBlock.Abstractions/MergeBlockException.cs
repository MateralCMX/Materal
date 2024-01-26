namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock异常
    /// </summary>
    public class MergeBlockException : Exception
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MergeBlockException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public MergeBlockException(string? message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MergeBlockException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
