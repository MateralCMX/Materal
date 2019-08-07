using AutoMapper;
using Demo.PresentationModel.Student.Request;
using Demo.Service.Model.Student;

namespace Demo.PresentationModel.AutoMapperProfile
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<QueryStudentFilterRequestModel, QueryStudentFilterModel>();
            CreateMap<AddStudentRequestModel, AddStudentModel>();
            CreateMap<EditStudentRequestModel, EditStudentModel>();
        }
    }
}
