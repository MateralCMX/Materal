using Materal.MergeBlock.Abstractions.Models;
using Materal.MergeBlock.Abstractions.WebModule.Models;

namespace Materal.MergeBlock.AutoMapperProfile
{
    /// <summary>
    /// MergeBlockAutoMapper映射配置
    /// </summary>
    public partial class MergeBlockProfile : Profile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MergeBlockProfile()
        {
            CreateMap<ExchangeIndexRequestModel, ExchangeIndexModel>();
            CreateMap<ExchangeParentRequestModel, ExchangeParentModel>();
            CreateMap<TimeQuantumAndOperationUserIDRequestModel, TimeQuantumAndOperationUserIDModel>();
            CreateMap<TimeQuantumRequestModel, TimeQuantumModel>();
            CreateMap<TimeQuantumRequestPageModel, TimeQuantumPageModel>();
        }
    }
}
