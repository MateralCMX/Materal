using AutoMapper;
using Demo.DataTransmitModel.Class;
using Demo.Domain;

namespace Demo.ServiceImpl.AutoMapperProfile
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<Class, ClassDTO>();
            CreateMap<Class, ClassListDTO>();
        }
    }
}
