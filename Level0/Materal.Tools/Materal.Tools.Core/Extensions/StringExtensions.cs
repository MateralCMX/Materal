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
#if DEBUG
            if (result.Exists)
            {
                if (result.Name == "Nupkgs") return result;
                if (result.Name == "Publish")
                {
                    result.Delete(true);
                }
            }
#else
            if (result.Exists)
            {
                string newPath = $"{path}_{DateTime.Now:yyyyMMddHHmmss}";
                result.MoveTo(newPath);
                result = new(path);
            }
#endif
            result.Create();
            result.Refresh();
            return result;
        }
    }
}
