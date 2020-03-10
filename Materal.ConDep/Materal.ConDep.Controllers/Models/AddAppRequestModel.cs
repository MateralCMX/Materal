namespace Materal.ConDep.Controllers.Models
{
    public class AddAppRequestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string AppPath { get; set; }
        /// <summary>
        /// 主模组名称
        /// </summary>
        public string MainModuleName { get; set; }
        /// <summary>
        /// 运行参数
        /// </summary>
        public string Parameters { get; set; }
    }
}
