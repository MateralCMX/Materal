#nullable enable
using System.IO;
using System.Text;

namespace MateralMergeBlockVSIX.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 保存为文件
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="solutionItem"></param>
        /// <param name="paths"></param>
        public static void SaveAs(this StringBuilder stringBuilder, SolutionItem? solutionItem, params string[] paths)
        {
            if(solutionItem is null || paths.Length < 1) return;
            DirectoryInfo directoryInfo = GetGeneratorCodeRootDirectory(solutionItem);
            string filePath = directoryInfo.FullName;
            for (int i = 0; i < paths.Length - 1; i++)
            {
                filePath = Path.Combine(filePath, paths[i]);
            }
            directoryInfo = new(filePath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                directoryInfo.Refresh();
            }
            filePath = Path.Combine(filePath, paths[^1]);
            string content = stringBuilder.ToString();
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }
        /// <summary>
        /// 获得生成代码的根目录
        /// </summary>
        /// <param name="solutionItem"></param>
        /// <returns></returns>
        public static DirectoryInfo GetGeneratorCodeRootDirectory(this SolutionItem solutionItem)
        {
            string path = Path.GetDirectoryName(solutionItem.FullPath);
            path = Path.Combine(path, "MGC");
            DirectoryInfo directoryInfo = new(path);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                directoryInfo.Refresh();
            }
            return directoryInfo;
        }
    }
}
