namespace Materal.Extensions
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 转换为Base64字符串
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ConvertToBase64String(string filePath) => new FileInfo(filePath).GetBase64String();
        /// <summary>
        /// 是否是图片文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="imageTyp"></param>
        /// <returns></returns>
        public static bool IsImageFile(string filePath, out string? imageTyp) => new FileInfo(filePath).IsImageFile(out imageTyp);
        /// <summary>
        /// 是否是图片文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsImageFile(string filePath) => new FileInfo(filePath).IsImageFile();
        /// <summary>
        /// 获取Base64图片
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ConvertToBase64Image(string filePath) => new FileInfo(filePath).GetBase64Image();
        /// <summary>
        /// 获得MD5(32位)签名
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isLower"></param>
        /// <returns></returns>
        public static string GetFileMd5_32(string filePath, bool isLower = false) => new FileInfo(filePath).GetMd5_32(isLower);
        /// <summary>
        /// 获得MD5(16位)签名
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isLower"></param>
        /// <returns></returns>
        public static string GetFileMd5_16(string filePath, bool isLower = false) => new FileInfo(filePath).GetMd5_16(isLower);
    }
}
