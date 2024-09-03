namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 控制器接口模型
    /// </summary>
    /// <param name="codes"></param>

    public class IControllerModel(string[] codes) : InterfaceModel(codes)
    {
        /// <summary>
        /// 领域名称
        /// </summary>
        public string DomainName => Name[1..^10];
    }
}
