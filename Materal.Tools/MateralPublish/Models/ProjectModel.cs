namespace MateralPublish.Models
{
    public class ProjectModel
    {
        /// <summary>
        /// 项目文件夹信息
        /// </summary>
        public DirectoryInfo ProjectDirectoryInfo { get; }
        public ProjectModel(string directoryPath)
        {
            ProjectDirectoryInfo = new DirectoryInfo(directoryPath);
            if (!ProjectDirectoryInfo.Exists) throw new Exception("项目不存在");
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="publishDirectoryInfo"></param>
        /// <param name="version"></param>
        public void Publish(DirectoryInfo publishDirectoryInfo, string version)
        {
            //TODO:调用dotnet build
            //移动Nuget包到更目录
        }
    }
}
