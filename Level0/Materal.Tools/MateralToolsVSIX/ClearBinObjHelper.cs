#nullable enable
using System.Collections.Generic;
using System.IO;

namespace MateralToolsVSIX
{
    public static class ClearBinObjHelper
    {
        public static List<string> ClearBinObj(string projectFullName)
        {
            List<string> messages = [];
            if (!projectFullName.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))
            {
                messages.Add($"\"{projectFullName}\"不是项目文件");
                return messages;
            }
            DirectoryInfo directoryInfo = new(Path.GetDirectoryName(projectFullName));
            foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
            {
                if (subDirectoryInfo.Name != "bin" && subDirectoryInfo.Name != "obj") continue;
                try
                {
                    subDirectoryInfo.Delete(true);
                }
                catch (Exception ex)
                {
                    messages.Add($"删除文件夹失败:{subDirectoryInfo.FullName}");
                    messages.Add(ex.Message);
                }
            }
            return messages;
        }
    }
}
