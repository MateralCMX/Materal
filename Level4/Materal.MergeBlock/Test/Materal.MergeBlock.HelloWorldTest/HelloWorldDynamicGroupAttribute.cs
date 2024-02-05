using Materal.MergeBlock.Abstractions.WebModule.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Materal.MergeBlock.HelloWorldTest
{
    /// <summary>
    /// HelloWorld动态分组
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HelloWorldDynamicGroupAttribute : Attribute, IDynamicGroup
    {
        /// <summary>
        /// 获取组名
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public string GetGroupName(ControllerModel controller) => "HelloWorldWebModule";
    }
}
