using AutoMapper;
using Materal.Oscillator.Abstractions.DataTransmitModel;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Models.Schedule;
using Materal.Oscillator.DR.Models;

namespace Materal.Oscillator.AutoMapperProfile
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<ScheduleModel, Schedule>();
            CreateMap<AddScheduleModel, Schedule>();
            CreateMap<EditScheduleModel, Schedule>();
            CreateMap<QueryScheduleManagerModel, QueryScheduleModel>();
            CreateMap<Schedule, ScheduleDTO>();
            CreateMap<Schedule, ScheduleFlowModel>();
        }
    }
}
