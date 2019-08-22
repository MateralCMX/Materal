using System.Threading.Tasks;

namespace Materal.TFMS.EventBus
{
    public interface IDynamicIntegrationEventHandler
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        Task HandleAsync(dynamic eventData);
    }
}
