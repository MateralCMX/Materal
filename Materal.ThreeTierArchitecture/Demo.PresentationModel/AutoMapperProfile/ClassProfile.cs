using AutoMapper;
using Demo.PresentationModel.Class.Request;
using Demo.Service.Model.Class;

namespace Demo.PresentationModel.AutoMapperProfile
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<QueryClassFilterRequestModel, QueryClassFilterModel>();
            CreateMap<AddClassRequestModel, AddClassModel>();
            CreateMap<EditClassRequestModel, EditClassModel>();
        }
    }
}
