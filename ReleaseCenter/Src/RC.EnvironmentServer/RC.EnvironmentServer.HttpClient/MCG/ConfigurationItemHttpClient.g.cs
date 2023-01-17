#nullable enable
using RC.Core.HttpClient;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.Common.Utils.TreeHelper;
using Materal.BaseCore.Common.Utils.IndexHelper;
using Materal.BaseCore.PresentationModel;
using Materal.Model;

namespace RC.EnvironmentServer.HttpClient
{
    public partial class ConfigurationItemHttpClient : HttpClientBase<AddConfigurationItemRequestModel, EditConfigurationItemRequestModel, QueryConfigurationItemRequestModel, ConfigurationItemDTO, ConfigurationItemListDTO>
    {
        public ConfigurationItemHttpClient() : base("RC.EnvironmentServer") { }
    }
}