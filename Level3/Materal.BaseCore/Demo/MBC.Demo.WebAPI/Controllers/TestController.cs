using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBC.Demo.WebAPI.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [AutoDI]
    public partial class TestController : MateralCoreWebAPIControllerBase
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public ResultModel<ITestModel> Test()
        {
            return ResultModel<ITestModel>.Success(new TestModel1()
            {
                Name = "Name",
                Name2 = "Name2"
            }, "Success");
        }
    }
    /// <summary>
    /// 测试模型
    /// </summary>
    public interface ITestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
    }
    /// <summary>
    /// 测试模型1
    /// </summary>
    public class TestModel1 : ITestModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 名称2
        /// </summary>
        public string Name2 { get; set; } = string.Empty;
    }
}
