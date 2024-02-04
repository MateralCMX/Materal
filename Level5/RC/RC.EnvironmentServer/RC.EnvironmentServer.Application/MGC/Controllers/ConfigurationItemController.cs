﻿using RC.EnvironmentServer.Abstractions.DTO.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.Services.Models.ConfigurationItem;

namespace RC.EnvironmentServer.Application.Controllers
{
    /// <summary>
    /// 配置项控制器
    /// </summary>
    public partial class ConfigurationItemController : MergeBlockControllerBase<AddConfigurationItemRequestModel, EditConfigurationItemRequestModel, QueryConfigurationItemRequestModel, AddConfigurationItemModel, EditConfigurationItemModel, QueryConfigurationItemModel, ConfigurationItemDTO, ConfigurationItemListDTO, IConfigurationItemService>, IConfigurationItemController
    {
    }
}
