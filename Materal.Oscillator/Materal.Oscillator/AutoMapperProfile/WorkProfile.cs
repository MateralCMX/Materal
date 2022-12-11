using AutoMapper;
using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.DataTransmitModel;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Models.Work;
using Materal.Oscillator.Abstractions.Models.WorkEvent;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.AutoMapperProfile
{
    public class WorkProfile : Profile
    {
        public WorkProfile()
        {
            CreateMap<AddWorkModel, Work>()
                .ForMember(m => m.WorkType, m => m.MapFrom(n => ConvertToWorkType(n.WorkData)))
                .ForMember(m => m.WorkData, m => m.MapFrom(n => ConvertToWorkData(n.WorkData)));
            CreateMap<EditWorkModel, Work>();
            CreateMap<QueryWorkEventManagerModel, QueryWorkModel>();
            CreateMap<Work, WorkDTO>()
                .ForMember(m => m.WorkData, m => m.MapFrom(n => OscillatorConvertHelper.ConvertToInterface<IWork>(n.WorkType, n.WorkData)));
            CreateMap<WorkModel, ScheduleWorkView>()
                .ForMember(m => m.WorkName, m => m.MapFrom(n => n.Name))
                .ForMember(m => m.WorkType, m => m.MapFrom(n => ConvertToWorkType(n.WorkData)))
                .ForMember(m => m.WorkData, m => m.MapFrom(n => ConvertToWorkData(n.WorkData)));
        }
        private static string ConvertToWorkType(IWork work) => work.GetType().Name;
        private static string ConvertToWorkData(IWork work) => work.Serialize();
    }
}
