namespace Materal.HttpGenerator
{
    public interface IGeneratorBuild
    {
        /// <summary>
        /// 消息
        /// </summary>
        event Action<string>? OnMessage;
        /// <summary>
        /// 输出路径
        /// </summary>
        string OutputPath { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        string ProjectName { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        string PrefixName { get; set; }
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
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        void SaveFile(string path, string fileName, string content);
    }
}