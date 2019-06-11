using System.Threading.Tasks;

namespace Materal.Dispatcher.QuartzNet
{
    public interface IDispatcherManager
    {
        Task Start(DispatcherConfigModel config);
        Task Stop();
    }
}
