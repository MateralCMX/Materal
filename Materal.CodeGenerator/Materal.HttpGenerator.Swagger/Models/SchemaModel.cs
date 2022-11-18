using Materal.ConvertHelper;
using Newtonsoft.Json.Linq;

namespace Materal.HttpGenerator.Swagger.Models
{
    public class SchemaModel : PropertyModel
    {
        /// <summary>
        /// 必填
        /// </summary>
        public List<string>? Required { get; set; }
        public List<int>? @Enum { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public Dictionary<string, PropertyModel>? Properties { get; set; }
        public SchemaModel(JToken source) : base(source)
        {
            foreach (JToken item in source)
            {
                if (item is not JProperty property) continue;
                if (property.Name == "properties")
                {
                    Properties = new();
                    foreach (JToken? propertyValue in property.Value)
                    {
                        if (propertyValue == null || propertyValue is not JProperty tempProperty) continue;
                        Properties.Add(tempProperty.Name, new PropertyModel(tempProperty.Value));
                    }
                }
                else if (property.Name == "enum")
                {
                    @Enum = new();
                    foreach (JToken? propertyValue in property.Value)
                    {
                        if (propertyValue == null) return;
                        if (propertyValue.CanConvertTo(typeof(int)))
                        {
                            @Enum.Add(propertyValue.ConvertTo<int>());
                        }
                    }
                }
                else if (property.Name == "required")
                {
                    Required = new();
                    foreach (JToken? propertyValue in property.Value)
                    {
                        if (propertyValue == null) return;
                        if (propertyValue.CanConvertTo(typeof(string)))
                        {
                            Required.Add(propertyValue.ConvertTo<string>());
                        }
                    }
                }
            }
        }
    }
}
