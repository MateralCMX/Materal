namespace MateralPublish.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 获得新文件夹信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DirectoryInfo GetNewDirectoryInfo(this string path)
        {
            DirectoryInfo result = new(path);
            if (result.Exists)
            {
                result.Delete(true);
            }
            result.Create();
            result.Refresh();
            return result;
        }
    }
}
