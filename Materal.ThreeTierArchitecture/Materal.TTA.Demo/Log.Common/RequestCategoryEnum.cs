using System.ComponentModel;

namespace Log.Common
{
    public enum RequestCategoryEnum
    {
        /// <summary>
        /// GET请求
        /// </summary>
       [Description("Get")]
        Get = 0,

        /// <summary>
        /// POST请求
        /// </summary>
        [Description("Post")]
        Post = 1,

        /// <summary>
        /// PUT请求
        /// </summary>
        [Description("Put")]
        Put = 2,

        /// <summary>
        /// DELETE请求
        /// </summary>
        [Description("Delete")]
        Delete = 3,

        /// <summary>
        /// Socket请求
        /// </summary>
        [Description("Socket")]
        Socket = 4
    }
}
