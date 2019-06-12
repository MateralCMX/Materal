using System.Threading.Tasks;

namespace Materal.Dispatcher.Server
{
    public interface IDispatcherServer
    {
        Task Start();
        Task Stop();
    }
}
