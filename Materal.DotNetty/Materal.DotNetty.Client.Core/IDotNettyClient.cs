using Materal.DotNetty.Common;
using System;
using System.Threading.Tasks;

namespace Materal.DotNetty.Client.Core
{
    public interface IDotNettyClient
    {
        /// <summary>
        /// ������Ϣ
        /// </summary>
        event Action<string> OnMessage;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        event Action<string, string> OnSubMessage;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        event Action<Exception> OnException;
        /// <summary>
        /// ��ȡ����
        /// </summary>
        event Func<string> OnGetCommand;
        /// <summary>
        /// 
        /// </summary>
        event Action<IClientChannelHandler> OnConfigHandler;
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        Task RunAsync(ClientConfig clientConfig);
        /// <summary>
        /// ֹͣ����
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
    }
}
