using Materal.Utils.Model;

namespace RC.ServerCenter.Web
{
    public class SelectDataModel
    {
        public string? Value { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class SelectDataModel<T>
        where T : struct, Enum
    {
        public T? Value { get; set; }
        public string Name { get; set; } = string.Empty;

        public static List<SelectDataModel<TEnum>> GetListAndAll<TEnum>(List<KeyValueModel<TEnum>> keyValues, string allName = "所有")
            where TEnum : struct, Enum
        {
            List<SelectDataModel<TEnum>> result = new()
            {
                new SelectDataModel<TEnum>()
                {
                    Value = default,
                    Name = allName
                }
            };
            foreach (KeyValueModel<TEnum> keyValue in keyValues)
            {
                result.Add(new()
                {
                    Value = keyValue.Key,
                    Name = keyValue.Value
                });
            }
            return result;
        }
        public static List<SelectDataModel<TEnum>> GetListAndAll<TEnum>(string allName = "所有")
            where TEnum : struct, Enum
        {
            List<KeyValueModel<TEnum>> keyValues = KeyValueModel<TEnum>.GetAllCode();
            return GetListAndAll(keyValues, allName);
        }
        public static List<SelectDataModel<TEnum>> GetList<TEnum>(List<KeyValueModel<TEnum>> keyValues)
            where TEnum : struct, Enum
        {
            List<SelectDataModel<TEnum>> result = new();
            foreach (KeyValueModel<TEnum> keyValue in keyValues)
            {
                result.Add(new()
                {
                    Value = keyValue.Key,
                    Name = keyValue.Value
                });
            }
            return result;
        }
        public static List<SelectDataModel<TEnum>> GetList<TEnum>()
            where TEnum : struct, Enum
        {
            List<KeyValueModel<TEnum>> keyValues = KeyValueModel<TEnum>.GetAllCode();
            return GetList(keyValues);
        }
    }
}
