namespace Materal.Extensions
{
    /// <summary>
    /// 文件信息扩展
    /// </summary>
    public static class FileInfoExtensions
    {
        /// <summary>
        /// 获取Base64字符串
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static string GetBase64String(this FileInfo fileInfo)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException("文件不存在", fileInfo.FullName);
            using FileStream fileStream = fileInfo.OpenRead();
            return fileStream.ToBase64();
        }
        /// <summary>
        /// 是否是图片文件
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="imageType"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static bool IsImageFile(this FileInfo fileInfo, out string? imageType)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException("文件不存在", fileInfo.FullName);
            using FileStream fileStream = fileInfo.OpenRead();
            return fileStream.IsImage(out imageType);
        }
        /// <summary>
        /// 是否是图片文件
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static bool IsImageFile(this FileInfo fileInfo)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException("文件不存在", fileInfo.FullName);
            using FileStream fileStream = fileInfo.OpenRead();
            return fileStream.IsImage();
        }
        /// <summary>
        /// 获取Base64图片
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ExtensionException"></exception>
        public static string GetBase64Image(this FileInfo fileInfo)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException("文件不存在", fileInfo.FullName);
            using FileStream fileStream = fileInfo.OpenRead();
            return fileStream.ToBase64Image();
        }
        /// <summary>
        /// 获得MD5(32位)签名
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="isLower"></param>
        /// <returns></returns>
        public static string GetMd5_32(this FileInfo fileInfo, bool isLower = false)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException("文件不存在", fileInfo.FullName);
            using FileStream fileStream = fileInfo.OpenRead();
            return fileStream.ToMd5_32Encode(isLower);
        }
        /// <summary>
        /// 获得MD5(16位)签名
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="isLower"></param>
        /// <returns></returns>
        public static string GetMd5_16(this FileInfo fileInfo, bool isLower = false)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException("文件不存在", fileInfo.FullName);
            using FileStream fileStream = fileInfo.OpenRead();
            return fileStream.ToMd5_16Encode(isLower);
        }
    }
}
