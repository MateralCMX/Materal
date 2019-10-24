namespace Materal.Services.Models
{
    public interface IFileModel
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        string FileName { get; set; }
        /// <summary>
        /// 文件摘要
        /// </summary>
        string FileAbstract { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        int Size { get; set; }
        /// <summary>
        /// 自动释放
        /// </summary>
        bool AutoDestroy { get; }
        /// <summary>
        /// 是否加载完毕
        /// </summary>
        bool CanComplete { get; }
        /// <summary>
        /// 文件主体
        /// </summary>
        byte[] FileContent { get; }
        /// <summary>
        /// 加载Buffy
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        void LoadBuffer(byte[] buffer, int index);
    }
}
