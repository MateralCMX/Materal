using Materal.BaseCore.AutoDI;

namespace Materal.BaseCore.AutoDITest
{
    [UsesVerify]
    public class GeneratorTests
    {
        [Fact]
        public async Task ServiceImplDIGeneratorTest()
        {
            var source = @"using AutoDITest.R;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.BaseCore.CodeGenerator;
using Materal.Utils.Model;
using MBC.Demo.DataTransmitModel.User;
using MBC.Demo.HttpClient;
using MBC.Demo.PresentationModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBC.Demo.WebAPI.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [Materal.BaseCore.CodeGenerator.AutoDI]
    public partial class TestController : MateralCoreWebAPIControllerBase
    {
        private readonly UserHttpClient _userHttpClient;
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public async Task<ResultModel> Test()
        {
            List<UserListDTO>? userInfos = await _userHttpClient.GetDataAsync(new QueryUserRequestModel()
            {
                PageIndex = 1,
                PageSize = 10,
            });
            if(userInfos != null)
            {
                return ResultModel<List<UserListDTO>>.Success(userInfos, ""测试成功"");
            }
            else
            {
                return ResultModel.Fail(""测试失败"");
            }
        }
    }
}
";
            AutoDIGenerator generator = new();
            await TestHelper.Verify(source, generator);
        }
    }
}
