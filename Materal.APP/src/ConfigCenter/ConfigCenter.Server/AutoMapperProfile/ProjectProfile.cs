using AutoMapper;
using ConfigCenter.PresentationModel.Project;
using ConfigCenter.Services.Models.Project;

namespace ConfigCenter.Server.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class ProjectProfile : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public ProjectProfile()
        {
            CreateMap<AddProjectRequestModel, AddProjectModel>();
            CreateMap<EditProjectRequestModel, EditProjectModel>();
            CreateMap<QueryProjectFilterRequestModel, QueryProjectFilterModel>();
        }
    }
}
