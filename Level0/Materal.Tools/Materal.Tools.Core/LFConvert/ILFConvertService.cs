namespace Materal.Tools.Core.LFConvert
{
    /// <summary>
    /// LF转换器
    /// </summary>
    public interface ILFConvertService
    {
        /// <summary>
        /// CRLF转换为LF
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task CRLFToLFAsync(string path, LFConvertOptions? options = null);
        /// <summary>
        /// LF转换为CRLF
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task LFToCRLFAsync(string path, LFConvertOptions? options = null);
    }
}
