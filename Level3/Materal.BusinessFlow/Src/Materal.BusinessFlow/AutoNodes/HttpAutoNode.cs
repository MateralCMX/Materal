using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.AutoNodes;
using Materal.BusinessFlow.AutoNodes.Base;
using Materal.Utils.Http;

namespace Materal.BusinessFlow.AutoNodes
{
    public class HttpAutoNode : BaseAutoNode, IAutoNode
    {
        private readonly IHttpHelper _httpHelper;
        public HttpAutoNode(IServiceProvider serviceProvider, IHttpHelper httpHelper) : base(serviceProvider)
        {
            _httpHelper = httpHelper;
        }
        public override async Task ExcuteAsync(AutoNodeModel autoNodeModel)
        {
            if (autoNodeModel.Node.Data == null || string.IsNullOrWhiteSpace(autoNodeModel.Node.Data)) return;
            HttpAutoNodeDataModel nodeData = autoNodeModel.Node.Data.JsonToObject<HttpAutoNodeDataModel>();
            string httpResultMessage = nodeData.Method switch
            {
                "GET" => await _httpHelper.SendGetAsync(nodeData.Url, nodeData.QueryParams, nodeData.Body, nodeData.Headers),
                "POST" => await _httpHelper.SendPostAsync(nodeData.Url, nodeData.QueryParams, nodeData.Body, nodeData.Headers),
                "PUT" => await _httpHelper.SendPutAsync(nodeData.Url, nodeData.QueryParams, nodeData.Body, nodeData.Headers),
                "DELETE" => await _httpHelper.SendDeleteAsync(nodeData.Url, nodeData.QueryParams, nodeData.Body, nodeData.Headers),
                "PATCH" => await _httpHelper.SendPatchAsync(nodeData.Url, nodeData.QueryParams, nodeData.Body, nodeData.Headers),
                _ => throw new BusinessFlowException("未知的请求方法")
            };
            if (!httpResultMessage.IsJson()) throw new BusinessFlowException("返回结果不是Json");
            //JObject httpResultJson = httpResultMessage.JsonToObject<JObject>();
            //await SaveDataAsync(httpResultJson, nodeData.ResultMappers);
        }
        //private async Task SaveDataAsync(JObject data, List<HttpAutoNodeMapperModel> mappers)
        //{
        //    await Task.Delay(100);
        //}
    }
}
