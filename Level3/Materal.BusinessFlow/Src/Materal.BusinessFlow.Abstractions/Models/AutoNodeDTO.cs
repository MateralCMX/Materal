using System.ComponentModel;
using System.Reflection;

namespace Materal.BusinessFlow.Abstractions.Models
{
    /// <summary>
    /// 自动节点数据传输模型
    /// </summary>
    public class AutoNodeDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string ID { get; set; } = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public AutoNodeDTO(Type autoNodeType)
        {
            const string autoNodeSuffix = "AutoNode";
            ID = autoNodeType.Name;
            DescriptionAttribute descriptionAttribute = autoNodeType.GetCustomAttribute<DescriptionAttribute>();
            Name = descriptionAttribute != null ? descriptionAttribute.Description : autoNodeType.Name;
            if (Name.EndsWith("AutoNode"))
            {
                Name = Name[..^autoNodeSuffix.Length];
            }
        }
    }
}
