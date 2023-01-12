using Materal.Common;
using System.Reflection;

namespace Materal.BaseCore.HttpClient.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 获得查询模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputModel"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static (bool isQuery, T queryModel) GetQueryModel<T>(this object inputModel, int pageSize = 10, int pageIndex = 1)
            where T : new()
        {
            bool isQuery = false;
            T queryModel = new();
            Type inputType = inputModel.GetType();
            Type queryType = queryModel.GetType();
            foreach (PropertyInfo inputPropertyInfo in inputType.GetProperties())
            {
                QueryPropertyAttribute? queryPropertyAttribute = inputPropertyInfo.GetCustomAttribute<QueryPropertyAttribute>();
                if (queryPropertyAttribute == null) continue;
                if (!string.IsNullOrWhiteSpace(queryPropertyAttribute.TypeName) && queryPropertyAttribute.TypeName != queryType.Name) continue;
                PropertyInfo? queryPropertyInfo = queryType.GetProperty(queryPropertyAttribute.TargetProperty);
                if (queryPropertyInfo == null) continue;
                if (queryPropertyInfo.PropertyType != inputPropertyInfo.PropertyType) continue;
                object? value = inputPropertyInfo.GetValue(inputModel);
                if (value == null || value.IsNullOrWhiteSpaceString()) continue;
                queryPropertyInfo.SetValue(queryModel, value);
                isQuery = true;
            }
            if (isQuery)
            {
                queryModel.SetPageInfo(pageSize, pageIndex);
            }
            return (isQuery, queryModel);
        }
        /// <summary>
        /// 设置分页信息
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        public static void SetPageInfo(this object queryModel, int pageSize, int pageIndex)
        {
            Type queryType = queryModel.GetType();
            queryType.GetProperty("PageIndex")?.SetValue(queryModel, pageIndex);
            queryType.GetProperty("PageSize")?.SetValue(queryModel, pageSize);
        }
    }
}
