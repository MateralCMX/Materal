using Materal.MergeBlock.Web.Abstractions.Controllers;

namespace Materal.MergeBlock.Web.Abstractions.ControllerHttpHelper
{
    /// <summary>
    /// 控制器Http帮助类
    /// </summary>
    public interface IControllerHttpHelper
    {
        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="moduleName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        string GetUrl(string projectName, string moduleName, string controllerName, string actionName);
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="TController"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="projectName"></param>
        /// <param name="moduleName"></param>
        /// <param name="methodName"></param>
        /// <param name="queryArgs"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        Task<TResult> SendAsync<TController, TResult>(string projectName, string moduleName, string methodName, Dictionary<string, string> queryArgs, params object[] datas)
            where TController : IMergeBlockController;
        /// <summary>
        /// 获取请求头
        /// </summary>
        /// <param name="hasToken"></param>
        /// <returns></returns>
        Dictionary<string, string> GetHeaders(bool hasToken);
        /// <summary>
        /// 获取服务名称
        /// </summary>
        /// <returns></returns>
        string GetServiceName();
    }
}
