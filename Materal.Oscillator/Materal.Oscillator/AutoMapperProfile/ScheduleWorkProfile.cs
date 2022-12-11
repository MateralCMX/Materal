using AutoMapper;
using Materal.Oscillator.Abstractions.DataTransmitModel;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.Models.ScheduleWork;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.AutoMapperProfile
{
    public class ScheduleWorkProfile : Profile
    {
        public ScheduleWorkProfile()
        {
            CreateMap<ScheduleWorkModel, ScheduleWork>();
            CreateMap<AddScheduleWorkModel, ScheduleWork>();
            CreateMap<EditScheduleWorkModel, ScheduleWork>();
            CreateMap<QueryScheduleWorkManagerModel, QueryScheduleWorkModel>();
            CreateMap<ScheduleWorkView, ScheduleWorkDTO>()
                .ForMember(m => m.WorkData, m => m.MapFrom(n => OscillatorConvertHelper.ConvertToInterface<IWork>(n.WorkType, n.WorkData)));
            CreateMap<ScheduleWorkView, Work>()
                .ForMember(m => m.WorkData, m => m.MapFrom(n => n.WorkData))
                .ForMember(m => m.WorkType, m => m.MapFrom(n => n.WorkType))
                .ForMember(m => m.ID, m => m.MapFrom(n => n.WorkID))
                .ForMember(m => m.CreateTime, m => m.MapFrom(n => n.CreateTime))
                .ForMember(m => m.Name, m => m.MapFrom(n => n.WorkName));
        }
    }
}
