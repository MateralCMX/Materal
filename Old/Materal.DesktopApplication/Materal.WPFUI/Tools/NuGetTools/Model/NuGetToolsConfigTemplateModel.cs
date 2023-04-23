namespace Materal.WPFUI.Tools.NuGetTools.Model
{
    /// <summary>
    /// NuGetTools配置文件模板模型
    /// </summary>
    public class NuGetToolsConfigTemplateModel
    {
        /// <summary>
        /// 项目地址
        /// </summary>
        public string ProjectAddress { get; set; }
        /// <summary>
        /// 目标地址
        /// </summary>
        public string TargetAddress { get; set; }
        /// <summary>
        /// 采用Debug版本标识
        /// </summary>
        public bool Debug { get; set; }
        /// <summary>
        /// 采用Release版本标识
        /// </summary>
        public bool Release { get; set; }
        /// <summary>
        /// 采用NuGet包
        /// </summary>
        public bool NuGet { get; set; }
        /// <summary>
        /// 采用DLL文件
        /// </summary>
        public bool DLL { get; set; }
    }
}
