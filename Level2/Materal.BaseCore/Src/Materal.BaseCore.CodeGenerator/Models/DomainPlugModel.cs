namespace Materal.BaseCore.CodeGenerator.Models
{
    public class DomainPlugModel
    {
        /// <summary>
        /// 公共项目
        /// </summary>
        public ProjectModel? CommonProject { get; set; }
        /// <summary>
        /// Domain项目
        /// </summary>
        public ProjectModel? DomainProject { get; set; }
        /// <summary>
        /// WebAPI项目
        /// </summary>
        public ProjectModel? WebAPIProject { get; set; }
        /// <summary>
        /// 服务项目
        /// </summary>
        public ProjectModel? ServicesProject { get; set; }
        /// <summary>
        /// 服务实现项目
        /// </summary>
        public ProjectModel? ServiceImplProject { get; set; }
        /// <summary>
        /// EF仓储项目
        /// </summary>
        public ProjectModel? EFRepositoryProject { get; set; }
        /// <summary>
        /// 数据传输模型项目
        /// </summary>
        public ProjectModel? DataTransmitModelProject { get; set; }
        /// <summary>
        /// 表现模型项目
        /// </summary>
        public ProjectModel? PresentationModelProject { get; set; }
        /// <summary>
        /// 枚举项目
        /// </summary>
        public ProjectModel? EnumsProject { get; set; }
        /// <summary>
        /// 实体对象
        /// </summary>
        public DomainModel Domain { get; set; } = new();
        /// <summary>
        /// 实体对象列表
        /// </summary>
        public List<DomainModel> Domains { get; set; } = new();
        /// <summary>
        /// 枚举列表
        /// </summary>
        public List<EnumModel>? Enums { get; set; } = new();
    }
}
