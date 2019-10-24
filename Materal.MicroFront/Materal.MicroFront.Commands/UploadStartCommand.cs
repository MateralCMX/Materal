namespace Materal.MicroFront.Commands
{
    /// <summary>
    /// 上传开始命令
    /// </summary>
    public class UploadStartCommand : Command
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Abstract { get; set; }
    }
}
