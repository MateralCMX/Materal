namespace Materal.Logger.LoggerHandlers.Models
{
    public class FileModel
    {
        /// <summary>
        /// 文件内容
        /// </summary>
        public string FileContent { get; set; } = string.Empty;
        /// <summary>
        /// 保存路径
        /// </summary>
        public string Path { get; set; } = string.Empty;
    }
}
