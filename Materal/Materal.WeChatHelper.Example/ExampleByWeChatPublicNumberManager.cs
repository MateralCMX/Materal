using Materal.Common;
using Materal.WeChatHelper.Model;
using Materal.WeChatHelper.Model.Basis.Result;
using Materal.WeChatHelper.Model.Material;
using Materal.WeChatHelper.Model.Material.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Materal.WeChatHelper.Example
{
    public class ExampleByWeChatPublicNumberManager
    {
        private readonly WeChatConfigModel _weChatConfig;
        public ExampleByWeChatPublicNumberManager(string appID, string appSecret)
        {
            _weChatConfig = new WeChatConfigModel
            {
                APPID = appID,
                APPSECRET = appSecret
            };
        }

        #region 基础服务
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken()
        {
            var manager = new WeChatPublicNumberManager(_weChatConfig);
            AccessTokenResultModel result = manager.GetAccessToken();
            ConsoleWriteProperties(result);
            return result.AccessToken;
        }
        #endregion
        public void GetWeChatIPAddress(string accessToken)
        {
            var manager = new WeChatPublicNumberManager(_weChatConfig);
            List<string> result = manager.GetWeChatIPAddress(accessToken);
            foreach (string ipAddress in result)
            {
                Console.WriteLine($"微信服务器IP为:{ipAddress}");
            }
        }
        public void NetworkDetection(string accessToken)
        {
            var manager = new WeChatPublicNumberManager(_weChatConfig);
            NetworkDetectionResultModel result = manager.NetworkDetection(accessToken);
            foreach (NetworkDetectionDNSModel dns in result.DNS)
            {
                Console.WriteLine($"微信服务器IP为:{dns.IP},服务商:{dns.RealOperator.GetDescription()}");
            }
            foreach (NetworkDetectionPingModel ping in result.Ping)
            {
                Console.WriteLine($"微信服务器IP为:{ping.IP},服务商:{ping.FromOperator.GetDescription()},丢包率:{ping.PackageLoss},耗时:{ping.Time}");
            }
        }
        public void CreateMenu(string accessToken)
        {
            var manager = new WeChatPublicNumberManager(_weChatConfig);
            var model = new CreateMenuModel();
            var firstMenu = new DefaultMenuButtonModel
            {
                name = "菜单一"
            };
            firstMenu.sub_button.Add(new ClickMenuButtonModel
            {
                name = "click",
                key = "clickKey"
            });
            firstMenu.sub_button.Add(new ViewMenuButtonModel
            {
                name = "view",
                url = "http://www.soso.com/"
            });
            firstMenu.sub_button.Add(new MiniProgramMenuButtonModel
            {
                name = "miniprogram",
                url = "http://www.soso.com/",
                appid = "wx4b55d7249ec22918",
                pagepath = "View/User/Login"
            });
            firstMenu.sub_button.Add(new ScanCodePushMenuButtonModel
            {
                name = "scancode_push",
                key = "scancode_pushKey"
            });
            firstMenu.sub_button.Add(new ScanCodeWaitMsgMenuButtonModel
            {
                name = "scancode_waitmsg",
                key = "scancode_waitmsgKey"
            });
            var secondMenu = new DefaultMenuButtonModel
            {
                name = "菜单二"
            };
            secondMenu.sub_button.Add(new PicSysPhotoMenuButtonModel
            {
                name = "pic_sysphoto",
                key = "pic_sysphotoKey"
            });
            secondMenu.sub_button.Add(new PicPhotoOrAlbumMenuButtonModel
            {
                name = "pic_photo_or_album",
                key = "pic_photo_or_albumKey"
            });
            secondMenu.sub_button.Add(new PicWeiXinMenuButtonModel
            {
                name = "pic_weixin",
                key = "pic_weixinKey"
            });
            var thirdMenu = new DefaultMenuButtonModel
            {
                name = "菜单三"
            };
            thirdMenu.sub_button.Add(new LocationSelectMenuButtonModel
            {
                name = "location_select",
                key = "location_selectKey"
            });
            //thirdMenu.sub_button.Add(new MediaIDMenuButtonModel
            //{
            //    name = "media_id",
            //    media_id = "media_id"
            //});
            //thirdMenu.sub_button.Add(new ViewLimitedMenuButtonModel
            //{
            //    name = "view_limited",
            //    media_id = "media_id"
            //});
            model.button.Add(firstMenu);
            model.button.Add(secondMenu);
            model.button.Add(thirdMenu);
            manager.CreateMenu(accessToken, model);
            Console.WriteLine("创建菜单成功");
        }
        #region 素材管理
        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <param name="accessToken"></param>
        public void AddTemporaryMaterial(string accessToken)
        {
            var manager = new WeChatPublicNumberManager(_weChatConfig);
            using (FileStream fileStream = File.OpenRead(@"D:\SystemFiles\Pictures\头像.jpg"))
            {
                var formItemModel = new FormItemModel
                {
                    Key = "F",
                    FileName = "头像.jpg",
                    FileContent = fileStream
                };
                AddTemporaryMaterialResultModel result = manager.AddTemporaryMaterial(accessToken, MaterialTypeEnum.Image, formItemModel);
                ConsoleWriteProperties(result);
            }
        }
        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <param name="accessToken"></param>
        public void GetTemporaryMaterial(string accessToken)
        {
            var manager = new WeChatPublicNumberManager(_weChatConfig);
            //AddTemporaryMaterialResultModel result = 
            manager.GetTemporaryMaterial(accessToken, "jmN3U2UJFxbqQ1e7rzSrkvQSJvdNqIBWo7mcrAEzkdobQFQSv5u3PqW7wF326nO9");
            //ConsoleWriteProperties(result);
        }
        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <param name="accessToken"></param>
        public void GetMaterialCount(string accessToken)
        {
            var manager = new WeChatPublicNumberManager(_weChatConfig);
            GetMaterialCountResultModel result = manager.GetMaterialCount(accessToken);
            ConsoleWriteProperties(result);
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 控制台输出属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="weChatResult"></param>
        private void ConsoleWriteProperties<T>(T weChatResult) where T : class
        {
            Type resultType = typeof(T);
            foreach (PropertyInfo propertyInfo in resultType.GetProperties())
            {
                Console.WriteLine($"{weChatResult.GetDescription(propertyInfo.Name)}为:{propertyInfo.GetValue(weChatResult)}");
            }
        }

        #endregion
    }
}
