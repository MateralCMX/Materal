using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.Domain;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;
using RC.EnvironmentServer.Services.Models.ConfigurationItem;

namespace RC.EnvironmentServer.WebAPI.AutoMapperProfile
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
