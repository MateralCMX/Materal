using System.Text;

namespace Materal.EmailHelper.Example
{
    public class ExampleByEmailManager
    {
        #region SendMaill
        public void SendMail()
        {
            var config = new EmailConfigModel
            {
                Host = "smtp2.materalcmx.com",
                UserName = "MateralMail",
                Password = "3391496"
            };
            var cacheManager = new EmailManager(config);
            const string address = "cmx@materalcmx.com";
            const string displayName = "Materal";
            const string title = "测试邮件标题";
            const string body = "<div><p>测试邮件体</p></div>";
            var targetAddress = new []
            {
                "342860484@qq.com"
            };
            var ccAddress = new []
            {
                "719220259@qq.com"
            };
            cacheManager.SendMail(address, displayName, title, body, targetAddress, ccAddress, Encoding.UTF8);
        }

        #endregion
    }
}
