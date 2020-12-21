using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Materal.DotNetty.CommandBus;

namespace Materal.WebSocketClient.Core
{
    public interface IWebSocketClient
    {
        /// <summary>
        /// ״̬
        /// </summary>
        WebSocketState State { get; }
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
        /// ���յ�����
        /// </summary>
        event Action<string> OnEventMessage;
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
        /// ��������
        /// </summary>
        /// <returns></returns>
        Task ReconnectionAsync();
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
