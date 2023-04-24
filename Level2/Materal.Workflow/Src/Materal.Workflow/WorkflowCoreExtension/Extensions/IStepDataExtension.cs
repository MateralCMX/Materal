using Materal.Workflow.StepDatas;

namespace Materal.Workflow.WorkflowCoreExtension.Extensions
{
    /// <summary>
    /// IStepData扩展
    /// </summary>
    public static class IStepDataExtension
    {
        /// <summary>
        /// 获得数据值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stepData"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public static T GetBuildDataValue<T>(this IStepData stepData, string name)
        {
            Dictionary<string, object?> data = stepData.GetPropertyValue<Dictionary<string, object?>>("BuildData");
            if (!data.ContainsKey(name) || data[name] is not T result) throw new WorkflowException("获取StepData数据失败");
            return result;
        }
    }
}
