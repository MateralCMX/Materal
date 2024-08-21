using Materal.Utils.Wechat.Model;
using Materal.Utils.Wechat.Model.Result;

namespace Materal.Test.UtilsTests.WechatTests
{
    public abstract class WechatTest : MateralTestBase
    {
        /// <summary>
        /// 微信配置
        /// </summary>
        protected readonly WechatConfigModel Config = new()
        {
            //测试号
            APPID = "wx27bc6dd896ccafda",
            APPSECRET = "252987761685b4148f10efc36de2322c"
        };
        /// <summary>
        /// 陈明旭
        /// </summary>
        protected const string OpenID1 = "ofvx86DqF-HmxDL0_FV47ECZ1bjY";
        /// <summary>
        /// 张庚
        /// </summary>
        protected const string OpenID2 = "ofvx86Czl9x9YzdjJKgg0RRkqipw";
        /// <summary>
        /// 从微信获得AccessToken
        /// </summary>
        /// <returns></returns>
        protected abstract Task<AccessTokenResultModel> GetAccessTokenByWechatAsync();
        /// <summary>
        /// 获得AccessToken
        /// </summary>
        /// <returns></returns>
        protected async Task<string> GetAccessTokenAsync()
        {
            string? accessToken = null;
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AccessTokens");
            DirectoryInfo directoryInfo = new(directoryPath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                directoryInfo.Refresh();
            }
            string filePath = Path.Combine(directoryPath, $"{Config.APPID}.json");
            FileInfo fileInfo = new(filePath);
            string saveJson;
            AccessTokenSaveModel saveModel;
            if (fileInfo.Exists)
            {
                try
                {
                    saveJson = File.ReadAllText(filePath);
                    saveModel = saveJson.JsonToObject<AccessTokenSaveModel>();
                    if (saveModel.IsEffective())
                    {
                        accessToken = saveModel.AccessToken;
                    }
                }
                catch
                {
                    fileInfo.Delete();
                    accessToken = null;
                }
            }
            if (accessToken is null)
            {
                AccessTokenResultModel result = await GetAccessTokenByWechatAsync();
                saveModel = new(result);
                saveJson = saveModel.ToJson();
                File.WriteAllText(filePath, saveJson);
                accessToken = result.AccessToken;
            }
            return accessToken;
        }
        private class AccessTokenSaveModel
        {
            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreateTime { get; set; } = DateTime.Now;
            /// <summary>
            /// AccessToken
            /// </summary>
            public string AccessToken { get; set; } = string.Empty;
            /// <summary>
            /// 有效时间
            /// </summary>
            public int ExpiresIn { get; set; }
            public AccessTokenSaveModel()
            {
            }
            public AccessTokenSaveModel(AccessTokenResultModel result)
            {
                AccessToken = result.AccessToken;
                ExpiresIn = result.ExpiresIn;
            }
            /// <summary>
            /// 是否有效
            /// </summary>
            /// <returns></returns>
            public bool IsEffective()
            {
                DateTime effectiveTime = CreateTime.AddSeconds(ExpiresIn - 60);
                return effectiveTime >= DateTime.Now;
            }
        }
    }
}
