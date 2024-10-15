using AutoMapper;
using Materal.MergeBlock.Abstractions.Models;

namespace Materal.MergeBlock.AutoMapperProfile
{
    internal class MergeBlockDefaultModelProfile : Profile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MergeBlockDefaultModelProfile()
        {
            CreateMap<ExchangeIndexRequestModel, ExchangeIndexModel>();
            CreateMap<ExchangeParentRequestModel, ExchangeParentModel>();
            CreateMap<TimeQuantumAndOperationUserIDRequestModel, TimeQuantumAndOperationUserIDModel>();
            CreateMap<TimeQuantumRequestModel, TimeQuantumModel>();
            CreateMap<TimeQuantumRequestPageModel, TimeQuantumPageModel>();
        }
    }
}
