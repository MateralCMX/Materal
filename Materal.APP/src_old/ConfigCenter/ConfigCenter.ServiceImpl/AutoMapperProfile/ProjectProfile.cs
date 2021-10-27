using AutoMapper;
using ConfigCenter.DataTransmitModel.Project;
using ConfigCenter.Domain;

namespace ConfigCenter.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectListDTO>();
            CreateMap<Project, ProjectDTO>();
        }
    }
}
