using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Materal.Common;
using Materal.ConvertHelper;

namespace Materal.WeChatHelper.Model
{
    /// <summary>
    /// 微信支数据模型
    /// </summary>
    public class WeChatDataModel
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public WeChatDataModel()
        {
            Data = new SortedDictionary<string, object>();
        }
        /// <summary>
        /// 排序字典
        /// </summary>
        public SortedDictionary<string, object> Data { get; }
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetValue(string key, object value)
        {
            Data[key] = value;
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public object GetValue(string key)
        {
            Data.TryGetValue(key, out object obj);
            return obj;
        }
        /// <summary>
        /// 是否已设置值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>检测结果</returns>
        public bool IsSet(string key)
        {
            return GetValue(key) != null;
        }

        public object ToObject()
        {
            return Data;
        }
        /// <summary>
        /// 转换为XML
        /// </summary>
        /// <returns>XML结果</returns>
        /// <exception cref="WeChatException"></exception>
        public string ToXml()
        {
            if (Data.Count > 0)
            {
                string xml = "<xml>";
                foreach (KeyValuePair<string, object> pair in Data)
                {
                    if (pair.Value != null)
                    {
                        if (pair.Value is int)
                        {
                            xml += string.Format("<{0}>{1}</{0}>", pair.Key, pair.Value);
                        }
                        else if (pair.Value is string)
                        {
                            xml += string.Format("<{0}><![CDATA[{1}]]></{0}>", pair.Key, pair.Value);
                        }
                        else
                        {
                            throw new WeChatException("数据类型错误!只能为String或Int类型。");
                        }
                    }
                    else
                    {
                        throw new WeChatException("含有值为null的字段!");
                    }
                }
                xml += "</xml>";
                return xml;
            }
            throw new WeChatException("数据为空!");
        }
        /// <summary>
        /// 将xml转为字典并返回对象内部的数据
        /// </summary>
        /// <param name="xml">待转换的xm</param>
        /// <param name="key">商户支付密钥</param>
        /// <returns>经转换得到的字典</returns>
        /// <exception cref="WeChatException"></exception>
        public SortedDictionary<string, object> FromXml(string xml, string key)
        {
            if (string.IsNullOrEmpty(xml)) throw new WeChatException("待转换的XML为空");
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                var xe = (XmlElement) xn;
                Data[xe.Name] = xe.InnerText;
            }
            if (!(Data["return_code"] is string dataValue) || dataValue != "SUCCESS") return Data;
            if (!CheckSign(key)) throw new WeChatException("签名错误");
            return Data;
        }
        /// <summary>
        /// 字典转换URL参数
        /// </summary>
        /// <returns>URL参数(不包含sign)</returns>
        public string ToUrlParams()
        {
            string buff = "";
            const char spacer = '&';
            foreach (KeyValuePair<string, object> pair in Data)
            {
                if (pair.Value != null)
                {
                    if (pair.Key != "sign" && pair.Value.ToString() != "")
                    {
                        buff += $"{pair.Key}={pair.Value}{spacer}";
                    }
                }
                else
                {
                    throw new WeChatException("WxPayData内部含有值为null的字段!");
                }
            }
            return buff.Trim(spacer);
        }
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns>Json字符串</returns>
        public string ToJson()
        {
            return Data.ToJson();
        }
        /// <summary>
        /// 转换为Web页面字符串
        /// </summary>
        /// <returns>Web页面字符串</returns>
        public string ToWebPrintStr()
        {
            string str = "";
            foreach (KeyValuePair<string, object> pair in Data)
            {
                if (pair.Value != null)
                {
                    str += $"{pair.Key}={pair.Value}<br />";
                }
                else
                {
                    throw new WeChatException("含有值为null的字段!");
                }

            }
            return str;
        }
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="key">商户支付密钥</param>
        /// <returns>签名</returns>
        public string MakeSign(string key)
        {
            string str = ToUrlParams();
            //在string后加入API KEY
            str += "&key=" + key;
            //MD5加密
            MD5 md5 = MD5.Create();
            byte[] bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }
        /// <summary>
        /// 检测签名是否正确
        /// </summary>
        /// <param name="key">商户支付密钥</param>
        /// <returns>检测结果</returns>
        public bool CheckSign(string key)
        {
            if (!IsSet("sign") || GetValue("sign").IsNullOrEmptyString()) return false;
            string returnSign = GetValue("sign").ToString();
            return MakeSign(key) == returnSign;

        }
    }
}
