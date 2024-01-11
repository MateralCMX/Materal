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
#if DEBUG
            if (result.Exists && result.Name == "Nupkgs") return result;
#endif
            if (result.Exists)
            {
                string newPath = $"{path}_{DateTime.Now:yyyyMMddHHmmss}";
                result.MoveTo(newPath);
                result = new(path);
            }
            result.Create();
            result.Refresh();
            return result;
        }
    }
}
