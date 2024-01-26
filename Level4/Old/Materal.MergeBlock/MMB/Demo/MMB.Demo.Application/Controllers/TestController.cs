//using Materal.Oscillator.Abstractions;
//using Polly;

//namespace MMB.Demo.Application.Controllers
//{
//    /// <summary>
//    /// 测试控制器
//    /// </summary>
//    public class TestController(ITokenService tokenService, IEventBus eventBus, IOscillatorHost host) : MergeBlockControllerBase, ITestController
//    {
//        /// <summary>
//        /// 说Hello
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public ResultModel SayHello() => ResultModel.Success("Hello World!");
//        /// <summary>
//        /// 获取Token
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet, AllowAnonymous]
//        public ResultModel<string> GetToken()
//        {
//            string token = tokenService.GetToken(Guid.NewGuid());
//            return ResultModel<string>.Success(token, "获取成功");
//        }
//        /// <summary>
//        /// 发送事件总线消息
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public async Task<ResultModel> SendEventBusMessageAsync()
//        {
//            MyEvent @event = new() { Message = "Hello EventBus!" };
//            await eventBus.PublishAsync(@event);
//            return ResultModel.Success("发送成功");
//        }
//        /// <summary>
//        /// 测试调度器
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [HttpGet]
//        public async Task<ResultModel> RunNowScheduleAsync(Guid id)
//        {
//            await host.RunNowAsync(id);
//            return ResultModel.Success("启动成功");
//        }
//        /// <summary>
//        /// 抛出系统异常
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public ResultModel ThrowSystemException() => throw new NotImplementedException("测试异常");
//        /// <summary>
//        /// 抛出模块业务异常
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public ResultModel ThrowModuleException() => throw new MMBException("测试异常");
//    }
//}
