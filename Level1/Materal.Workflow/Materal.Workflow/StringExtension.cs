using Materal.Workflow.StepDatas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Reflection;

namespace Materal.Workflow
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Json转换为开始节点数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static StartStepData JsonToStartStepData(this string json)
        {
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(json) ?? throw new WorkflowException("反序列化Json失败");
            JToken topNode = jsonObject.Root;
            IStepData stepData = ConvertToStepData(topNode);
            if (stepData is not StartStepData result)
            {
                result = new StartStepData()
                {
                    Name = "开始节点",
                    Next = stepData
                };
            }
            return result;
        }
        private static IStepData ConvertToStepData(JToken topNode)
        {
            JToken stepDataTypeToken = topNode.FirstOrDefault(m => m is JProperty propertyNode && propertyNode.Name == nameof(IStepData.StepDataType)) ?? throw new WorkflowException($"获取{nameof(IStepData.StepDataType)}值失败");
            if (stepDataTypeToken is not JProperty stepDataTypeProperty || !stepDataTypeProperty.HasValues || stepDataTypeProperty.Value.Type != JTokenType.String) throw new WorkflowException($"获取{nameof(IStepData.StepDataType)}值失败");
            string stepDataTypeName = stepDataTypeProperty.Value.ToString();
            Type stepDataType = stepDataTypeName.GetTypeByTypeName<IStepData>() ?? throw new WorkflowException($"未找到类型{stepDataTypeName}");
            IStepData result = stepDataType.Instantiation<IStepData>(Array.Empty<object>());
            foreach (JToken node in topNode)
            {
                if (node.Path == nameof(IStepData.StepDataType) || node is not JProperty propertyNode) continue;
                PropertyInfo? propertyInfo = stepDataType.GetProperty(propertyNode.Name);
                if (propertyInfo == null || !propertyInfo.CanWrite) continue;
                try
                {
                    object? value = propertyNode.Value.Type switch
                    {
                        JTokenType.Null => null,
                        JTokenType.String => StringNodeToObject(propertyNode, propertyInfo),
                        JTokenType.Boolean => Convert.ToBoolean(propertyNode.Value),
                        JTokenType.Integer => Convert.ToInt32(propertyNode.Value),
                        JTokenType.Float => Convert.ToDecimal(propertyNode.Value),
                        JTokenType.Guid => Guid.Parse(propertyNode.Value.ToString()),
                        JTokenType.TimeSpan => TimeSpan.Parse(propertyNode.Value.ToString()),
                        JTokenType.Date => Convert.ToDateTime(propertyNode.Value),
                        JTokenType.Object => NodeToObject(propertyNode, propertyInfo),
                        JTokenType.Array => NodeToArray(propertyNode, propertyInfo),
                        _ => throw new WorkflowException($"不能转换类型{propertyNode.Value.Type}")
                    };
                    propertyInfo.SetValue(result, value);
                }
                catch (Exception ex)
                {
                    throw new WorkflowException($"设置属性值失败{propertyInfo.Name}->{propertyNode.Name}:{propertyNode.Value}", ex);
                }
            }
            return result;
        }
        private static object StringNodeToObject(JProperty propertyNode, PropertyInfo propertyInfo)
        {
            string result = propertyNode.Value.ToString();
            if (propertyInfo.PropertyType == typeof(TimeSpan))
            {
                return TimeSpan.Parse(result);
            }
            return result;
        }
        /// <summary>
        /// 节点转换为对象
        /// </summary>
        /// <param name="propertyNode"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        private static object NodeToObject(JProperty propertyNode, PropertyInfo propertyInfo)
        {
            bool isIStepData = propertyInfo.PropertyType == typeof(IStepData) || propertyInfo.PropertyType.IsAssignableTo(typeof(IStepData));
            if (isIStepData)
            {
                return ConvertToStepData(propertyNode.Value);
            }
            return propertyNode.Value.ToString().JsonToObject(propertyInfo.PropertyType);
        }
        /// <summary>
        /// 节点转换数组
        /// </summary>
        /// <param name="propertyNode"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        private static IEnumerable NodeToArray(JProperty propertyNode, PropertyInfo propertyInfo)
        {
            object? values = null;
            if (propertyInfo.PropertyType.IsGenericType)
            {
                if (propertyInfo.PropertyType.GenericTypeArguments.Length == 1)
                {
                    Type genericType = propertyInfo.PropertyType.GenericTypeArguments[0];
                    if (genericType == typeof(IStepData) || genericType.IsAssignableTo(typeof(IStepData)))
                    {
                        object tempObject = propertyInfo.PropertyType.Instantiation();
                        if(tempObject is IList list && propertyNode.Value.Type == JTokenType.Array)
                        {
                            foreach (JToken jToken in propertyNode.Value.Children())
                            {
                                IStepData tempStepData = ConvertToStepData(jToken);
                                list.Add(tempStepData);
                            }
                            values = list;
                        }
                    }
                }
            }
            if (values == null)
            {
                values = JsonConvert.DeserializeObject(propertyNode.Value.ToString(), propertyInfo.PropertyType);
            }
            if (values == null || values is not IEnumerable result) throw new WorkflowException($"不能转换类型{propertyNode.Value.Type}");
            return result;
        }
    }
}
