using Newtonsoft.Json.Linq;

namespace Materal.HttpGenerator.Swagger.Models
{
    public class ComponentModel
    {
        public Dictionary<string, SchemaModel> Schemas { get; set; } = new Dictionary<string, SchemaModel>();
        public ComponentModel(JToken source)
        {
            foreach (JToken? item in source)
            {
                if (item == null || item is not JProperty property) continue;
                switch (property.Name)
                {
                    case "schemas":
                        foreach (JProperty schemaSource in property.Value.Cast<JProperty>())
                        {
                            Schemas.Add(schemaSource.Name, new SchemaModel(schemaSource.Value));
                        }
                        break;
                    case "securitySchemes":
                        break;
                }
            }
        }
    }
}
