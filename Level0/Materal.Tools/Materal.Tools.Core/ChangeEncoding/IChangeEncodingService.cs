namespace Materal.Tools.Core.ChangeEncoding
{
    /// <summary>
    /// 更改编码服务
    /// </summary>
    public interface IChangeEncodingService
    {
        /// <summary>
        /// 更改编码
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task ChangeEncodingAsync(string path, ChangeEncodingOptions? options = null);
    }
}
