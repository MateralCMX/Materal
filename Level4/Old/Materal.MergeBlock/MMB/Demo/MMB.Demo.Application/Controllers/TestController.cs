//using Materal.Oscillator.Abstractions;
//using Polly;

//namespace MMB.Demo.Application.Controllers
//{
//    /// <summary>
//    /// ���Կ�����
//    /// </summary>
//    public class TestController(ITokenService tokenService, IEventBus eventBus, IOscillatorHost host) : MergeBlockControllerBase, ITestController
//    {
//        /// <summary>
//        /// ˵Hello
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public ResultModel SayHello() => ResultModel.Success("Hello World!");
//        /// <summary>
//        /// ��ȡToken
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet, AllowAnonymous]
//        public ResultModel<string> GetToken()
//        {
//            string token = tokenService.GetToken(Guid.NewGuid());
//            return ResultModel<string>.Success(token, "��ȡ�ɹ�");
//        }
//        /// <summary>
//        /// �����¼�������Ϣ
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public async Task<ResultModel> SendEventBusMessageAsync()
//        {
//            MyEvent @event = new() { Message = "Hello EventBus!" };
//            await eventBus.PublishAsync(@event);
//            return ResultModel.Success("���ͳɹ�");
//        }
//        /// <summary>
//        /// ���Ե�����
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [HttpGet]
//        public async Task<ResultModel> RunNowScheduleAsync(Guid id)
//        {
//            await host.RunNowAsync(id);
//            return ResultModel.Success("�����ɹ�");
//        }
//        /// <summary>
//        /// �׳�ϵͳ�쳣
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public ResultModel ThrowSystemException() => throw new NotImplementedException("�����쳣");
//        /// <summary>
//        /// �׳�ģ��ҵ���쳣
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public ResultModel ThrowModuleException() => throw new MMBException("�����쳣");
//    }
//}
