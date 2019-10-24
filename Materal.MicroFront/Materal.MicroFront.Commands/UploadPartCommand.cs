namespace Materal.MicroFront.Commands
{
    /// <summary>
    /// 上传部分命令
    /// </summary>
    public class UploadPartCommand : Command
    {
        /// <summary>
        /// 数据
        /// </summary>
        public string Base64Buffer { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; set; }
    }
}
