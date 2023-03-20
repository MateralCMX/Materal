using AutoMapper;
using Materal.BaseCore.PresentationModel;
using Materal.BaseCore.Services.Models;

namespace XMJ.Authority.WebAPI.AutoMapperProfile
{
    public class MateralBaseCoreProfile : Profile
    {
        public MateralBaseCoreProfile()
        {
            CreateMap<ExchangeIndexRequestModel, ExchangeIndexModel>();
            CreateMap<TimeQuantumAndOperationUserIDRequestModel, TimeQuantumAndOperationUserIDModel>();
            CreateMap<TimeQuantumRequestModel, TimeQuantumModel>();
            CreateMap<TimeQuantumRequestPageModel, TimeQuantumPageModel>();
        }
    }
}
