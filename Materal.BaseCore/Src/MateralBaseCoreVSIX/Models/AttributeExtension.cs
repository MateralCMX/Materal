using System.Collections.Generic;
using System.Linq;
using System;

namespace MateralBaseCoreVSIX.Models
{
    public static class AttributeExtension
    {
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
        /// <typeparam name="T"></typeparam>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this DomainPropertyModel property) where T : Attribute => property.Attributes.HasAttribute<T>();
        /// <summary>
        /// 是否拥有特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static bool HasAttribute<T>(this List<AttributeModel> attributes) where T : Attribute => attributes.GetAttribute<T>() != null;
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static AttributeModel GetAttribute<T>(this DomainModel domain) where T : Attribute => domain.Attributes.GetAttribute<T>();
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static AttributeModel GetAttribute<T>(this DomainPropertyModel property) where T : Attribute => property.Attributes.GetAttribute<T>();
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        //public static AttributeModel GetAttribute(this DomainModel domain, string attributeName) => domain.Attributes.GetAttribute(attributeName);
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <param name="property"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        //public static AttributeModel GetAttribute(this DomainPropertyModel property, string attributeName) => property.Attributes.GetAttribute(attributeName);
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static AttributeModel GetAttribute<T>(this List<AttributeModel> attributes) where T : Attribute
        {
            string tName = typeof(T).Name;
            return attributes.GetAttribute(tName);
        }
        /// <summary>
        /// 获得特性
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static AttributeModel GetAttribute(this List<AttributeModel> attributes, string attributeName)
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
        /// 获得Attribute参数
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static AttributeArgumentModel GetAttributeArgument(this AttributeModel attribute, string target = null)
        {
            if (attribute.AttributeArguments == null || attribute.AttributeArguments.Count <= 0) return null;
            if (string.IsNullOrWhiteSpace(target))
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
                return attributeName.Substring(0, attributeName.Length - attributeSuffix.Length);
            }
            return attributeName;
        }
    }
}
