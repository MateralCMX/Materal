using Materal.MergeBlock.GeneratorCode.Attributers;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 服务接口模型
    /// </summary>
    /// <param name="codes"></param>

    public class IServiceModel(string[] codes) : InterfaceModel(codes)
    {
        /// <summary>
        /// 领域名称
        /// </summary>
        public string DomainName => Name[1..^7];
        /// <summary>
        /// 是否有Mapper方法
        /// </summary>
        public bool HasMapperMethod => Methods.Count > 0 && Methods.Any(m => m.Attributes.HasAttribute<MapperControllerAttribute>());
    }
}
