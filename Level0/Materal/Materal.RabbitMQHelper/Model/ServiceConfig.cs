using System;
using System.Text;

namespace Materal.RabbitMQHelper.Model
{
    public class ServiceConfig: IServiceConfig
    {
        public string HostName { get; set; } = "localhost";
        public int Port { get; set; } = -1;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30000);
        public Encoding Encoding { get; set; } = Encoding.UTF8;
    }
}
