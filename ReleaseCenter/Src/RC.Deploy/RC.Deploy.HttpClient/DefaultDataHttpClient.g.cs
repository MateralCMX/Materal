using RC.Core.HttpClient;
using RC.Deploy.DataTransmitModel.DefaultData;
using RC.Deploy.PresentationModel.DefaultData;
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.Common.Utils.TreeHelper;
using Materal.BaseCore.Common.Utils.IndexHelper;
using Materal.Model;

namespace RC.Deploy.HttpClient
{
    public class DefaultDataHttpClient : HttpClientBase<AddDefaultDataRequestModel, EditDefaultDataRequestModel, QueryDefaultDataRequestModel, DefaultDataDTO, DefaultDataListDTO>
    {
        public DefaultDataHttpClient() : base("RC.Deploy") { }
    }
}
