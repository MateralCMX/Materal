using AutoMapper;
using Authority.PresentationModel.Incident.Request;
using Authority.Service.Model.Incident;
namespace Authority.PresentationModel.AutoMapperProfile
{
    /// <summary>
    /// 事件AutoMapper配置
    /// </summary>
    public sealed class IncidentProfile : Profile
    {
        /// <summary>
        /// 事件AutoMapper配置
        /// </summary>
        public IncidentProfile()
        {
            CreateMap<AddIncidentRequestModel, AddIncidentModel>();
            CreateMap<EditIncidentRequestModel, EditIncidentModel>();
            CreateMap<QueryIncidentFilterRequestModel, QueryIncidentFilterModel>();
        }
    }
}
