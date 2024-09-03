namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock模块异常
    /// </summary>
    public class MergeBlockModuleException : MergeBlockException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MergeBlockModuleException()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public MergeBlockModuleException(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MergeBlockModuleException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
