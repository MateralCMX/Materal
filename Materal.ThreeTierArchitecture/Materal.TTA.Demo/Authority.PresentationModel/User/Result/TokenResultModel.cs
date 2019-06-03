using System;
using System.Collections.Generic;
using System.Text;

namespace Authority.PresentationModel.User.Result
{
    /// <summary>
    /// Token返回模型
    /// </summary>
    public class TokenResultModel
    {
#pragma warning disable IDE1006 // 命名样式
        /// <summary>
        /// Token
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 有效秒数
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// Token类型
        /// </summary>
        public string token_type { get; set; }
#pragma warning restore IDE1006 // 命名样式
    }
}
