using AutoMapper;
using Materal.Oscillator.Abstractions.DataTransmitModel;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Models.WorkEvent;

namespace Materal.Oscillator.AutoMapperProfile
{
    public class WorkEventProfile : Profile
    {
        public WorkEventProfile()
        {
            CreateMap<WorkEventModel, WorkEvent>();
            CreateMap<AddWorkEventModel, WorkEvent>();
            CreateMap<EditWorkEventModel, WorkEvent>();
            CreateMap<QueryWorkEventManagerModel, QueryWorkEventModel>();
            CreateMap<WorkEventView, WorkEventDTO>();
        }
    }
}
