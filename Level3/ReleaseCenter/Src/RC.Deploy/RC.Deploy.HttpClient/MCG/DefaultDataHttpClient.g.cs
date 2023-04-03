#nullable enable
using RC.Core.HttpClient;
using Materal.BaseCore.PresentationModel;
using Materal.Utils.Model;
using RC.Deploy.DataTransmitModel.DefaultData;
using RC.Deploy.PresentationModel.DefaultData;

namespace RC.Deploy.HttpClient
{
    public partial class DefaultDataHttpClient : HttpClientBase<AddDefaultDataRequestModel, EditDefaultDataRequestModel, QueryDefaultDataRequestModel, DefaultDataDTO, DefaultDataListDTO>
    {
        public DefaultDataHttpClient() : base("RC.Deploy") { }
    }
}
