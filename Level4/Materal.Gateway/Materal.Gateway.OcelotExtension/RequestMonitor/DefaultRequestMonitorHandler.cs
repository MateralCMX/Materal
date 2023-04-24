using Materal.Gateway.OcelotExtension.RequestMonitor.Model;

namespace Materal.Gateway.OcelotExtension.RequestMonitor
{
    public class DefaultRequestMonitorHandler : IRequestMonitorHandler
    {
        public virtual void HandlerRequst(HandlerRequestModel model) { }
        public virtual Task HandlerRequstAsync(HandlerRequestModel model)
        {
            HandlerRequst(model);
            return Task.CompletedTask;
        }
        public virtual void HandlerResponse(HandlerResponseModel model) { }
        public virtual Task HandlerResponseAsync(HandlerResponseModel model)
        {
            HandlerResponse(model);
            return Task.CompletedTask;
        }
    }
}
