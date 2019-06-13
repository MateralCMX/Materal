namespace Common.Model.APIAuthorityConfig
{
    /// <summary>
    /// 微信服务API权限配置
    /// </summary>
    public static class WeChatServiceAPIAuthorityConfig
    {
        #region Application
        /// <summary>
        /// 开启微信应用服务
        /// </summary>
        public const string OpenApplicationCode = "OpenApplication";
        /// <summary>
        /// 应用操作
        /// </summary>
        public const string ApplicationOperationCode = "ApplicationOperation";
        /// <summary>
        /// 添加应用
        /// </summary>
        public const string AddApplicationCode = "AddApplication";
        /// <summary>
        /// 修改应用
        /// </summary>
        public const string EditApplicationCode = "EditApplication";
        /// <summary>
        /// 删除应用
        /// </summary>
        public const string DeleteApplicationCode = "DeleteApplication";
        /// <summary>
        /// 查询应用
        /// </summary>
        public const string QueryApplicationCode = "QueryApplication";
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
        #region WeChatMiniProgram
        /// <summary>
        /// 获得OpenID
        /// </summary>
        public const string WeChatMiniProgramServer = "WeChatMiniProgramServer";
        /// <summary>
        /// 获得OpenID
        /// </summary>
        public const string GetWeChatMiniProgramOpenIDCode = "GetWeChatMiniProgramOpenIDCode";
        #endregion
    }
}
