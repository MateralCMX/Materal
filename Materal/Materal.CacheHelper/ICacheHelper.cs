using Materal.DateTimeHelper;
using System;

namespace Materal.CacheHelper
{
    public interface ICacheHelper
    {
        void SetBySliding(string key, object content, double hours);
        void SetBySliding(string key, object content, double timer, DateTimeTypeEnum dateTimeType);
        void SetBySliding(string key, object content, DateTime date);
        void SetBySliding(string key, object content, TimeSpan timeSpan);
        void SetByAbsolute(string key, object content, double hours);
        void SetByAbsolute(string key, object content, double timer, DateTimeTypeEnum dateTimeType);
        void SetByAbsolute(string key, object content, DateTime date);
        void SetByAbsolute(string key, object content, TimeSpan timeSpan);
        object Get(string key);
        T Get<T>(string key);
        void Remove(string key);
        void Clear();
    }
}
