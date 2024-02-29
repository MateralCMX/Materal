using Materal.Utils.Wechat;
using Materal.Utils.Wechat.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml;

namespace WechatServerTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WechatController : ControllerBase
    {
        private const string _token = "MateralTestToken";
        private readonly WechatOffcialAccountServerHelper _helper;

        public WechatController(IServiceProvider serviceProvider)
        {
            _helper = new(_token, serviceProvider);
        }
        /// <summary>
        /// ����΢�ŷ���������֤����
        /// </summary>
        /// <param name="signature">΢�ż���ǩ��</param>
        /// <param name="timestamp">ʱ���</param>
        /// <param name="nonce">�����</param>
        /// <param name="echostr">����ַ���</param>
        /// <returns></returns>
        [HttpGet]
        public string Get(string signature, string timestamp, string nonce, string echostr)
        {
            if (!_helper.IsWechatRequest(timestamp, nonce, signature)) throw new Exception("����΢�ŵ�����");
            return echostr;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="signature">΢�ż���ǩ��</param>
        /// <param name="timestamp">ʱ���</param>
        /// <param name="nonce">�����</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] string signature, [FromQuery] string timestamp, [FromQuery] string nonce)
        {
            if (!_helper.IsWechatRequest(timestamp, nonce, signature)) throw new Exception("����΢�ŵ�����");
            try
            {
                XmlDocument xmlDocument = await GetBodyXmlAsync();
                ReplyMessageModel? replyMessage = await _helper.HandlerWechatEventAsync(xmlDocument);
                if (replyMessage is not null)
                {
                    return Content(replyMessage.GetXmlString(), "text/xml");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Content("success");
        }
        /// <summary>
        /// ���BodyXml�ַ���
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetBodyXmlStringAsync()
        {
            HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            using MemoryStream stream = new();
            await HttpContext.Request.Body.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            using StreamReader streamReader = new(stream, Encoding.UTF8);
            string bodyString = await streamReader.ReadToEndAsync();
            return bodyString;
        }
        /// <summary>
        /// ���BodyXml����
        /// </summary>
        /// <returns></returns>
        private async Task<XmlDocument> GetBodyXmlAsync()
        {
            string xmlString = await GetBodyXmlStringAsync();
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(xmlString);
            return xmlDocument;
        }
    }

}