using RC.EnvironmentServer.Abstractions.DTO.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.Services.Models.ConfigurationItem;

namespace RC.EnvironmentServer.Application.AutoMapperProfile
{
    public partial class ConfigurationItemProfile
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Init()
        {
            base.Init();
            CreateMap<ConfigurationItemListDTO, ConfigurationItem>()
                .ForMember(m => m.ID, n => n.Ignore());
            CreateMap<SyncConfigRequestModel, SyncConfigModel>();
        }
    }
}
