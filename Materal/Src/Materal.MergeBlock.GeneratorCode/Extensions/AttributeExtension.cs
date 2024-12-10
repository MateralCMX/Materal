using Materal.MergeBlock.GeneratorCode.Models;

namespace Materal.MergeBlock.GeneratorCode.Extensions
{
    /// <summary>
    /// 特性扩展
    /// </summary>
    public static class AttributeExtension
    {
        #region DomainModel
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this DomainModel domain) where T : Attribute => domain.Attributes.HasAttribute<T>();
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static bool HasAttribute<T1, T2>(this DomainModel domain)
            where T1 : Attribute
            where T2 : Attribute => domain.Attributes.HasAttribute<T1>() || domain.Attributes.HasAttribute<T2>();
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static bool HasAttribute<T1, T2, T3>(this DomainModel domain)
            where T1 : Attribute
            where T2 : Attribute
            where T3 : Attribute => domain.Attributes.HasAttribute<T1>() || domain.Attributes.HasAttribute<T2>() || domain.Attributes.HasAttribute<T3>();
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static bool HasAttribute<T1, T2, T3, T4>(this DomainModel domain)
            where T1 : Attribute
            where T2 : Attribute
            where T3 : Attribute
            where T4 : Attribute => domain.Attributes.HasAttribute<T1>() || domain.Attributes.HasAttribute<T2>() || domain.Attributes.HasAttribute<T3>() || domain.Attributes.HasAttribute<T4>();
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static AttributeModel? GetAttribute<T>(this DomainModel domain) where T : Attribute => domain.Attributes.GetAttribute<T>();
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static AttributeModel[] GetAttributes<T>(this DomainModel domain) where T : Attribute => domain.Attributes.GetAttributes<T>();
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static AttributeModel? GetAttribute(this DomainModel domain, string attributeName) => domain.Attributes.GetAttribute(attributeName);
        #endregion
        #region PropertyModel
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this PropertyModel property) where T : Attribute => property.Attributes.HasAttribute<T>();
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool HasAttribute<T1, T2>(this PropertyModel property)
            where T1 : Attribute
            where T2 : Attribute => property.Attributes.HasAttribute<T1>() || property.Attributes.HasAttribute<T2>();
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool HasAttribute<T1, T2, T3>(this PropertyModel property)
            where T1 : Attribute
            where T2 : Attribute
            where T3 : Attribute => property.Attributes.HasAttribute<T1>() || property.Attributes.HasAttribute<T2>() || property.Attributes.HasAttribute<T3>();
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool HasAttribute<T1, T2, T3, T4>(this PropertyModel property)
            where T1 : Attribute
            where T2 : Attribute
            where T3 : Attribute
            where T4 : Attribute => property.Attributes.HasAttribute<T1>() || property.Attributes.HasAttribute<T2>() || property.Attributes.HasAttribute<T3>() || property.Attributes.HasAttribute<T4>();
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static AttributeModel? GetAttribute<T>(this PropertyModel property) where T : Attribute => property.Attributes.GetAttribute<T>();
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static AttributeModel[] GetAttributes<T>(this PropertyModel property) where T : Attribute => property.Attributes.GetAttributes<T>();
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <param name="property"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static AttributeModel? GetAttribute(this PropertyModel property, string attributeName) => property.Attributes.GetAttribute(attributeName);
        #endregion
        #region EnumModel
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this EnumModel @enum) where T : Attribute => @enum.Attributes.HasAttribute<T>();
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static bool HasAttribute<T1, T2>(this EnumModel @enum)
            where T1 : Attribute
            where T2 : Attribute => @enum.Attributes.HasAttribute<T1>() || @enum.Attributes.HasAttribute<T2>();
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static bool HasAttribute<T1, T2, T3>(this EnumModel @enum)
            where T1 : Attribute
            where T2 : Attribute
            where T3 : Attribute => @enum.Attributes.HasAttribute<T1>() || @enum.Attributes.HasAttribute<T2>() || @enum.Attributes.HasAttribute<T3>();
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static bool HasAttribute<T1, T2, T3, T4>(this EnumModel @enum)
            where T1 : Attribute
            where T2 : Attribute
            where T3 : Attribute
            where T4 : Attribute => @enum.Attributes.HasAttribute<T1>() || @enum.Attributes.HasAttribute<T2>() || @enum.Attributes.HasAttribute<T3>() || @enum.Attributes.HasAttribute<T4>();
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static AttributeModel? GetAttribute<T>(this EnumModel @enum) where T : Attribute => @enum.Attributes.GetAttribute<T>();
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static AttributeModel[] GetAttributes<T>(this EnumModel @enum) where T : Attribute => @enum.Attributes.GetAttributes<T>();
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <param name="enum"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static AttributeModel? GetAttribute(this EnumModel @enum, string attributeName) => @enum.Attributes.GetAttribute(attributeName);
        #endregion
        #region AttributeModel
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this List<AttributeModel> attributes) where T : Attribute => attributes.GetAttribute<T>() != null;
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static bool HasAttribute(this List<AttributeModel> attributes, string attributeName) => attributes.GetAttribute(attributeName) != null;
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static AttributeModel? GetAttribute<T>(this List<AttributeModel> attributes) where T : Attribute
        {
            string tName = typeof(T).Name;
            return attributes.GetAttribute(tName);
        }
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static AttributeModel[] GetAttributes<T>(this List<AttributeModel> attributes) where T : Attribute
        {
            string tName = typeof(T).Name;
            return attributes.GetAttributes(tName);
        }
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static AttributeModel? GetAttribute(this List<AttributeModel> attributes, string attributeName)
        {
            string tNotSuffixName = attributeName.RemoveAttributeSuffix();
            if (attributeName == tNotSuffixName)
            {
                return attributes.FirstOrDefault(m => m.Name == attributeName);
            }
            else
            {
                return attributes.FirstOrDefault(m => m.Name == attributeName || m.Name == tNotSuffixName);
            }
        }
        /// <summary>
        /// 获得特性组
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static AttributeModel[] GetAttributes(this List<AttributeModel> attributes, string attributeName)
        {
            string tNotSuffixName = attributeName.RemoveAttributeSuffix();
            if (attributeName == tNotSuffixName)
            {
                return attributes.Where(m => m.Name == attributeName).ToArray();
            }
            else
            {
                return attributes.Where(m => m.Name == attributeName || m.Name == tNotSuffixName).ToArray();
            }
        }
        /// <summary>
        /// 获得Attribute参数
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static AttributeArgumentModel? GetAttributeArgument(this AttributeModel attribute, string? target = null)
        {
            if (attribute.AttributeArguments == null || attribute.AttributeArguments.Count <= 0) return null;
            if (target is null || string.IsNullOrWhiteSpace(target))
            {
                return attribute.AttributeArguments.FirstOrDefault(m => string.IsNullOrWhiteSpace(m.Target));
            }
            else
            {
                return attribute.AttributeArguments.FirstOrDefault(m => m.Target == target);
            }
        }
        /// <summary>
        /// 移除特性后缀
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static string RemoveAttributeSuffix(this string attributeName)
        {
            const string attributeSuffix = "Attribute";
            if (attributeName.EndsWith(attributeSuffix))
            {
                return attributeName[..^attributeSuffix.Length];
            }
            return attributeName;
        }
        /// <summary>
        /// 获得代码
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static string? GetCode(this List<AttributeModel> attributes)
        {
            if (attributes.Count <= 0) return null;
            List<string> attributeCodes = [];
            foreach (AttributeModel attribute in attributes)
            {
                string? item = attribute.ToString();
                if (string.IsNullOrWhiteSpace(item)) continue;
                attributeCodes.Add(item);
            }
            string code = $"[{string.Join(", ", attributeCodes)}]";
            return code;
        }
        #endregion
    }
}