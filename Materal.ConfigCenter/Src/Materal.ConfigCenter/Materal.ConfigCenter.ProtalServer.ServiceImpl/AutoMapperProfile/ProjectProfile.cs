using AutoMapper;
using Materal.ConfigCenter.ProtalServer.DataTransmitModel.Project;
using Materal.ConfigCenter.ProtalServer.Domain;

namespace Materal.ConfigCenter.ProtalServer.ServiceImpl.AutoMapperProfile
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
