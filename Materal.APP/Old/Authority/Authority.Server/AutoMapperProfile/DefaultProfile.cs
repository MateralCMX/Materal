using AutoMapper;
using Materal.Model;

namespace Authority.Server.AutoMapperProfile
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<PageModel, PageInfoModel>();
        }
    }
}
