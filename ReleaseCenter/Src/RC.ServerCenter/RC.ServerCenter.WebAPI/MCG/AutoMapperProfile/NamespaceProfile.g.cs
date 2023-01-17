using AutoMapper;
using RC.ServerCenter.PresentationModel.Namespace;
using RC.ServerCenter.Services.Models.Namespace;
using RC.ServerCenter.DataTransmitModel.Namespace;
using RC.ServerCenter.Domain;

namespace RC.ServerCenter.WebAPI.AutoMapperProfile
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
            CreateMap<EditNamespaceModel, Namespace>();
            CreateMap<AddNamespaceRequestModel, AddNamespaceModel>();
            CreateMap<EditNamespaceRequestModel, EditNamespaceModel>();
            CreateMap<Namespace, NamespaceListDTO>();
            CreateMap<Namespace, NamespaceDTO>();
            CreateMap<QueryNamespaceRequestModel, QueryNamespaceModel>();
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
