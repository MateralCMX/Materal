using Materal.Oscillator.Abstractions.Works;

namespace Materal.MergeBlock.Oscillator.Abstractions
{
    /// <summary>
    /// 调度器初始化管理器
    /// </summary>
    public static class MergeBlockWorkInitManager
    {
        private readonly static object _initKeyLock = new();
        private readonly static List<string> _initKey = [];
        /// <summary>
        /// 添加初始化标识
        /// </summary>
        /// <param name="workData"></param>
        public static void AddInitKey(IWorkData workData) => AddInitKey(workData.TypeName);
        /// <summary>
        /// 添加初始化标识
        /// </summary>
        /// <param name="key"></param>
        public static void AddInitKey(string key)
        {
            if (_initKey.Contains(key)) return;
            lock (_initKeyLock)
            {
                if (_initKey.Contains(key)) return;
                _initKey.Add(key);
            }
        }
        /// <summary>
        /// 移除初始化标识
        /// </summary>
        /// <param name="workData"></param>
        public static void RemoveInitKey(IWorkData workData) => RemoveInitKey(workData.TypeName);
        /// <summary>
        /// 移除初始化标识
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveInitKey(string key)
        {
            if (!_initKey.Contains(key)) return;
            lock (_initKeyLock)
            {
                if (!_initKey.Contains(key)) return;
                _initKey.Remove(key);
            }
        }
        /// <summary>
        /// 是否需要初始化
        /// </summary>
        /// <param name="workData"></param>
        /// <returns></returns>
        public static bool IsInit(IWorkData workData) => IsInit(workData.TypeName);
        /// <summary>
        /// 是否需要初始化
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsInit(string key) => _initKey.Contains(key);
    }
}