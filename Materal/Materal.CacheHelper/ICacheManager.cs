using Materal.DateTimeHelper;
using System;
using System.Collections.Generic;

namespace Materal.CacheHelper
{
    public interface ICacheManager
    {
        /// <summary>
        /// 设置缓存(只要有访问就会延时)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="content">要缓存的对象</param>
        /// <param name="hours">小时数</param>
        void SetBySliding(string key, object content, double hours);
        /// <summary>
        /// 设置缓存(只要有访问就会延时)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="content">要缓存的对象</param>
        /// <param name="timer">时间值</param>
        /// <param name="dateTimeType">时间类型</param>
        void SetBySliding(string key, object content, double timer, DateTimeTypeEnum dateTimeType);
        /// <summary>
        /// 设置缓存(只要有访问就会延时)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="content">要缓存的对象</param>
        /// <param name="date">到期时间</param>
        void SetBySliding(string key, object content, DateTime date);
        /// <summary>
        /// 设置缓存(只要有访问就会延时)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="content">要缓存的对象</param>
        /// <param name="timeSpan">时间段</param>
        void SetBySliding(string key, object content, TimeSpan timeSpan);
        /// <summary>
        /// 设置缓存(绝对时间)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="content">要缓存的对象</param>
        /// <param name="hours">小时数</param>
        void SetByAbsolute(string key, object content, double hours);
        /// <summary>
        /// 设置缓存(绝对时间)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="content">要缓存的对象</param>
        /// <param name="timer">时间值</param>
        /// <param name="dateTimeType">时间类型</param>
        void SetByAbsolute(string key, object content, double timer, DateTimeTypeEnum dateTimeType);
        /// <summary>
        /// 设置缓存(绝对时间)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="content">要缓存的对象</param>
        /// <param name="date">到期时间</param>
        void SetByAbsolute(string key, object content, DateTime date);
        /// <summary>
        /// 设置缓存(绝对时间)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="content">要缓存的对象</param>
        /// <param name="timeSpan">时间段</param>
        void SetByAbsolute(string key, object content, TimeSpan timeSpan);
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        object Get(string key);
        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        T Get<T>(string key);
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();
        /// <summary>
        /// 获得所有的缓存键
        /// </summary>
        /// <returns></returns>
        List<string> GetCacheKeys();
    }
}
