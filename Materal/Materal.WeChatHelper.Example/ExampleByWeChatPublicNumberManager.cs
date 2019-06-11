using System;
using System.Collections.Generic;
using Materal.WeChatHelper.Model;
using Materal.Common;

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
        public void GetAccessToken()
        {
            var manager = new WeChatPublicNumberManager(_weChatConfig);
            AccessTokenResultModel result = manager.GetAccessToken();
            Console.WriteLine($"AccessToken为:{result.AccessToken},有效期:{result.ExpiresIn}");
        }
        public void GetAccessToken(string accessToken)
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
    }
}
