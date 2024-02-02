using RC.ServerCenter.Abstractions.DTO.Namespace;
using RC.ServerCenter.Abstractions.RequestModel.Namespace;
using RC.ServerCenter.Abstractions.Services.Models.Namespace;

namespace RC.ServerCenter.Abstraction.AutoMapperProfile
{
    /// <summary>
    /// 命名空间AutoMapper映射配置基类
    /// </summary>
    public partial class NamespaceProfileBase : Profile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            CreateMap<AddNamespaceModel, Namespace>();
            CreateMap<AddNamespaceRequestModel, AddNamespaceModel>();
            CreateMap<EditNamespaceModel, Namespace>();
            CreateMap<EditNamespaceRequestModel, EditNamespaceModel>();
            CreateMap<QueryNamespaceRequestModel, QueryNamespaceModel>();
            CreateMap<Namespace, NamespaceListDTO>();
            CreateMap<Namespace, NamespaceDTO>();
        }
    }
    /// <summary>
    /// 命名空间AutoMapper映射配置
    /// </summary>
    public partial class NamespaceProfile : NamespaceProfileBase
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public NamespaceProfile()
        {
            Init();
        }
    }
}
