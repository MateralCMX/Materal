namespace Materal.HttpGenerator
{
    public interface IGeneratorBuild
    {
        /// <summary>
        /// 输出路径
        /// </summary>
        string OutputPath { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        string ProjectName { get; set; }
        /// <summary>
        /// 基础路径
        /// </summary>
        string BaseUrl { get; set; }
        /// <summary>
        /// 设置源
        /// </summary>
        /// <param name="soucre"></param>
        Task SetSourceAsync(string soucre);
        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        Task BuildAsync();
    }
}