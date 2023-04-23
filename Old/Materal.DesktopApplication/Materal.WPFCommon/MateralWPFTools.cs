using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Materal.WPFCommon
{
    public static class MateralWPFTools
    {
        /// <summary>
        /// 获取内部元素
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T GetDescendantByType<T>(DependencyObject element) where T : DependencyObject
        {
            Type tType = typeof(T);
            if (element == null) return null;
            if (element.GetType() == tType || element.GetType().BaseType == tType) return element as T;
            if (!(element is FrameworkElement frameworkElement)) return null;
            frameworkElement.ApplyTemplate();
            int cnt = VisualTreeHelper.GetChildrenCount(element);
            for (var i = 0; i < cnt; i++)
            {
                DependencyObject dependencyObject = VisualTreeHelper.GetChild(element, i);
                if (dependencyObject is Panel panel)
                {
                    foreach (UIElement panelChild in panel.Children)
                    {
                        DependencyObject descendantByType = GetDescendantByType<T>(panelChild);
                        if (descendantByType != null)
                        {
                            return (T)descendantByType;
                        }
                    }
                }
                else if (dependencyObject.GetType() == tType || dependencyObject.GetType().BaseType == tType)
                {
                    return (T)dependencyObject;
                }
            }
            return null;
        }
    }
}
