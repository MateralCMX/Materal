namespace Common.Model.APIAuthorityConfig
{
    /// <summary>
    /// 微信服务API权限配置
    /// </summary>
    public static class WeChatServiceAPIAuthorityConfig
    {
        #region 应用
        /// <summary>
        /// 开启微信应用服务
        /// </summary>
        public const string OpenApplicationCode = "OpenApplication";
        #endregion
        #region WeChatDomain
        /// <summary>
        /// 微信域名操作
        /// </summary>
        public const string WeChatDomainOperationCode = "WeChatDomainOperation";
        /// <summary>
        /// 添加微信域名
        /// </summary>
        public const string AddWeChatDomainCode = "AddWeChatDomain";
        /// <summary>
        /// 修改微信域名
        /// </summary>
        public const string EditWeChatDomainCode = "EditWeChatDomain";
        /// <summary>
        /// 删除微信域名
        /// </summary>
        public const string DeleteWeChatDomainCode = "DeleteWeChatDomain";
        /// <summary>
        /// 查询微信域名
        /// </summary>
        public const string QueryWeChatDomainCode = "QueryWeChatDomain";
        #endregion
    }
}
