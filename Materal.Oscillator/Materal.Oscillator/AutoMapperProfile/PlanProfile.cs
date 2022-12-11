using AutoMapper;
using Materal.Oscillator.Abstractions.DataTransmitModel;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Models.Plan;
using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.AutoMapperProfile
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            CreateMap<PlanModel, Plan>()
                .ForMember(m => m.PlanTriggerType, m => m.MapFrom(n => ConvertToPlanTriggerType(n.PlanTriggerData)))
                .ForMember(m => m.PlanTriggerData, m => m.MapFrom(n => ConvertToPlanTriggerData(n.PlanTriggerData)));
            CreateMap<AddPlanModel, Plan>()
                .ForMember(m => m.PlanTriggerType, m => m.MapFrom(n => ConvertToPlanTriggerType(n.PlanTriggerData)))
                .ForMember(m => m.PlanTriggerData, m => m.MapFrom(n => ConvertToPlanTriggerData(n.PlanTriggerData)));
            CreateMap<EditPlanModel, Plan>()
                .ForMember(m => m.PlanTriggerType, m => m.MapFrom(n => ConvertToPlanTriggerType(n.PlanTriggerData)))
                .ForMember(m => m.PlanTriggerData, m => m.MapFrom(n => ConvertToPlanTriggerData(n.PlanTriggerData)));
            CreateMap<QueryPlanManagerModel, QueryPlanModel>();
            CreateMap<PlanView, PlanDTO>()
                .ForMember(m => m.PlanTriggerData, m => m.MapFrom(n => OscillatorConvertHelper.ConvertToInterface<IPlanTrigger>(n.PlanTriggerType, n.PlanTriggerData)));
        }
        private static string ConvertToPlanTriggerType(IPlanTrigger planTrigger) => planTrigger.GetType().Name;
        private static string ConvertToPlanTriggerData(IPlanTrigger planTrigger) => planTrigger.Serialize();
    }
}
