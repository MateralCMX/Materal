using AutoMapper;
using Demo.DataTransmitModel.Student;
using Demo.Domain;

namespace Demo.ServiceImpl.AutoMapperProfile
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<Student, StudentListDTO>();
        }
    }
}
