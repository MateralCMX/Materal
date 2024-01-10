using Materal.MergeBlock.Application.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace MMB.Demo.Application.Controllers
{
    /// <summary>
    /// ���Կ�����
    /// </summary>
    public class TestController() : MergeBlockControllerBase
    {
        /// <summary>
        /// ˵Hello
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel SayHello() => ResultModel.Success("Hello World!");
        /// <summary>
        /// �׳�ϵͳ�쳣
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel ThrowSystemException() => throw new NotImplementedException("�����쳣");
        /// <summary>
        /// �׳�ģ��ҵ���쳣
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel ThrowModuleException() => throw new MergeBlockModuleException("�����쳣");
    }
}
