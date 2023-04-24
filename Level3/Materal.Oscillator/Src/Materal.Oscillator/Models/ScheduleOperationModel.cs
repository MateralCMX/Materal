using System.Reflection;

namespace Materal.Oscillator.Models
{
    public class ScheduleOperationModel
    {
        /// <summary>
        /// 业务领域
        /// </summary>
        public string Territory { get; set; } = "Public";
        /// <summary>
        /// 设置业务领域属性
        /// </summary>
        /// <param name="model"></param>
        public void SetTerritoryProperties(object model)
        {
            Type thisType = GetType();
            Type modelType = model.GetType();
            foreach (PropertyInfo propertyInfo in thisType.GetProperties())
            {
                PropertyInfo? targetPropertyInfo = modelType.GetProperty(propertyInfo.Name);
                if (targetPropertyInfo == null) continue;
                object? value = propertyInfo.GetValue(this);
                targetPropertyInfo.SetValue(model, value);
            }
        }
    }
}
