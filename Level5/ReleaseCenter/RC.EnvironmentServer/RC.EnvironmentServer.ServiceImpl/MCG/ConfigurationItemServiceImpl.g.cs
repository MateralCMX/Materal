using Materal.BaseCore.ServiceImpl;
using Materal.BaseCore.Services.Models;
using Materal.TTA.EFRepository;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.Domain;
using RC.EnvironmentServer.Domain.Repositories;
using RC.EnvironmentServer.Services;
using RC.EnvironmentServer.Services.Models.ConfigurationItem;

namespace RC.EnvironmentServer.ServiceImpl
{
    /// <summary>
    /// 配置项服务实现
    /// </summary>
    public partial class ConfigurationItemServiceImpl : BaseServiceImpl<AddConfigurationItemModel, EditConfigurationItemModel, QueryConfigurationItemModel, ConfigurationItemDTO, ConfigurationItemListDTO, IConfigurationItemRepository, ConfigurationItem>, IConfigurationItemService
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ConfigurationItemServiceImpl(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
