//using System.Collections.Concurrent;

//namespace Materal.MergeBlock.Abstractions.Oscillator
//{
//    /// <summary>
//    /// Oscillator数据帮助类
//    /// </summary>
//    public static class OscillatorDataHelper
//    {
//        private readonly static List<string> _initKey = [];
//        private readonly static object _initKeyLock = new();
//        private readonly static List<string> _initingKey = [];
//        private readonly static object _initingKeyLock = new();
//        private readonly static ConcurrentDictionary<string, object> _datas = new();
//        /// <summary>
//        /// 是否初始化
//        /// </summary>
//        /// <param name="workName"></param>
//        /// <param name="autoRemove"></param>
//        /// <returns></returns>
//        public static bool IsInit(string workName, bool autoRemove = true)
//        {
//            bool result = _initKey.Contains(workName);
//            if (result && autoRemove)
//            {
//                RemoveInitKey(workName);
//            }
//            return result;
//        }
//        /// <summary>
//        /// 移除初始化标识
//        /// </summary>
//        /// <param name="workName"></param>
//        public static void RemoveInitKey(string workName)
//        {
//            lock (_initKeyLock)
//            {
//                if (!_initKey.Contains(workName)) return;
//                _initKey.Remove(workName);
//            }
//        }
//        /// <summary>
//        /// 设置初始化键
//        /// </summary>
//        /// <param name="workName"></param>
//        public static void SetInitKey(string workName)
//        {
//            lock (_initKeyLock)
//            {
//                if (_initKey.Contains(workName)) return;
//                _initKey.Add(workName);
//            }
//        }
//        /// <summary>
//        /// 是否初始化中
//        /// </summary>
//        /// <param name="workName"></param>
//        /// <param name="autoRemove"></param>
//        /// <returns></returns>
//        public static bool IsIniting(string workName, bool autoRemove = true)
//        {
//            bool result = _initingKey.Contains(workName);
//            if (result && autoRemove)
//            {
//                RemoveInitingKey(workName);
//            }
//            return result;
//        }
//        /// <summary>
//        /// 移除初始化中标识
//        /// </summary>
//        /// <param name="workName"></param>
//        public static void RemoveInitingKey(string workName)
//        {
//            lock (_initingKeyLock)
//            {
//                if (!_initingKey.Contains(workName)) return;
//                _initingKey.Remove(workName);
//            }
//        }
//        /// <summary>
//        /// 设置初始化中键
//        /// </summary>
//        /// <param name="workName"></param>
//        public static void SetInitingKey(string workName)
//        {
//            lock (_initingKeyLock)
//            {
//                if (_initingKey.Contains(workName)) return;
//                _initingKey.Add(workName);
//            }
//        }
//        /// <summary>
//        /// 设置数据
//        /// </summary>
//        /// <typeparam name="TSchedule"></typeparam>
//        /// <param name="data"></param>
//        public static void SetData<TSchedule>(object data)
//            where TSchedule : IOscillatorSchedule, new() => SetData<TSchedule, object>(data);
//        /// <summary>
//        /// 设置数据
//        /// </summary>
//        /// <typeparam name="TSchedule"></typeparam>
//        /// <typeparam name="TData"></typeparam>
//        /// <param name="data"></param>
//        public static void SetData<TSchedule, TData>(TData data)
//            where TSchedule : IOscillatorSchedule, new() => SetData(new TSchedule().WorkName, data);
//        /// <summary>
//        /// 设置数据
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="workName"></param>
//        /// <param name="data"></param>
//        public static void SetData<T>(string workName, T data)
//        {
//            if (data == null) return;
//            if (_datas.ContainsKey(workName))
//            {
//                _datas.TryAdd(workName, data);
//            }
//            else
//            {
//                _datas[workName] = data;
//            }
//        }
//        /// <summary>
//        /// 获得数据
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="workName"></param>
//        /// <param name="autoRemove"></param>
//        /// <returns></returns>
//        public static T? GetData<T>(string workName, bool autoRemove = true)
//        {
//            if (!_datas.TryGetValue(workName, out object? value)) return default;
//            if (value is not T result) return default;
//            if (autoRemove)
//            {
//                RemoveData(workName);
//            }
//            return result;
//        }
//        /// <summary>
//        /// 移除数据
//        /// </summary>
//        /// <param name="workName"></param>
//        public static void RemoveData(string workName)
//        {
//            if (!_datas.ContainsKey(workName)) return;
//            _datas.Remove(workName, out _);
//        }
//    }
//}
