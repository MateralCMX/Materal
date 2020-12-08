using System;
using System.Threading.Tasks;
using Materal.DotNetty.CommandBus;

namespace Materal.WebSocketClient.Core
{
    public interface IWebSocketClient
    {
        /// <summary>
        /// ���ӳɹ�
        /// </summary>
        event Action OnConnectionSuccess;
        /// <summary>
        /// ����ʧ��
        /// </summary>
        event Action OnConnectionFail;
        /// <summary>
        /// ���ӹر�
        /// </summary>
        event Action OnClose;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        event Action<string, string> OnSubMessage;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        event Action<Exception> OnException;
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        Task RunAsync();
        /// <summary>
        /// ֹͣ����
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task SendCommandAsync(ICommand command);
    }
}
