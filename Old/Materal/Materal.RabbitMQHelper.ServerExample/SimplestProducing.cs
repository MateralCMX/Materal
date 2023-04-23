namespace Materal.RabbitMQHelper.ServerExample
{
    /// <summary>
    /// 简单生产者
    /// </summary>
    public class SimplestProducing
    {
        private readonly ProducingService _producingService;

        public SimplestProducing()
        {
            _producingService = new ServiceFactory().GetSimplestProducingService("MateralDemoSimplestExchange", "MateralDemoSimplestQueue01");
        }
        /// <summary>
        /// 启动
        /// </summary>
        public void Run()
        {
            for (var i = 0; i < 20000; i++)
            {
                _producingService.SendMessage($"Hello World{i}!");
            }
        }
    }
}
