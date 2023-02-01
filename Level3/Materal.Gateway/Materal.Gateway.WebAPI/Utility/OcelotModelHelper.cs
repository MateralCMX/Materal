using Ocelot.Configuration.File;
using System;
using System.Collections;
using System.Reflection;

namespace Materal.Gateway.WebAPI.Utility
{
    public static class OcelotModelHelper
    {
        public static FileRoute GetNewFileRoute()
        {
            FileRoute result = new FileRoute();
            SetEmptyString(result);
            return result;
        }

        private static void SetEmptyString(object obj)
        {
            Type objType = obj.GetType();
            foreach (PropertyInfo propertyInfo in objType.GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    object value = propertyInfo.GetValue(obj);
                    if (value == null)
                    {
                        propertyInfo.SetValue(obj, string.Empty);
                    }
                }
                else if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType.GetInterface(nameof(ICollection)) == null)
                {
                    SetEmptyString(propertyInfo.GetValue(obj));
                }
            }
        }
    }
}
