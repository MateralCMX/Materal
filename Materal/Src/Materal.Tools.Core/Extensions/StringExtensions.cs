namespace Materal.Tools.Core.Extensions
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
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
                if (result.Name == "Nupkgs") return result;
                result.Delete(true);
            }
            result.Create();
            result.Refresh();
            return result;
        }
    }
}
