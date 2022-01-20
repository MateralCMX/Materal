using System.Threading.Tasks;

namespace MPB.Service
{
    public interface IProjectManage
    {
        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="modelPath"></param>
        /// <param name="projectConfig"></param>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        Task<bool> CreateProjectAsync(string modelPath, ProjectConfigModel projectConfig, string outputPath);
    }
}
