using System;
using Template.NuGet;

namespace Template.Common
{
    public class AlipayConfig
    {
        /// <summary>
        /// 应用ID,您的APPID
        /// </summary>
        public static string AppId = ConfigHelper.GetAppConfig("Alipay:AppID");

        /// <summary>
        /// 合作商户uid
        /// </summary>
        public static string Uid = "";

        /// <summary>
        /// 支付宝网关
        /// </summary>
        public static string Gatewayurl = ConfigHelper.GetAppConfig("Alipay:GatewayUrl");

        /// <summary>
        /// 商户私钥，您的原始格式RSA私钥
        /// </summary>
        public static string PrivateKey = ConfigHelper.GetAppConfig("Alipay:PrivateKey");

        /// <summary>
        /// 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
        /// </summary>
        public static string AlipayPublicKey = ConfigHelper.GetAppConfig("Alipay:PublicKey");

        /// <summary>
        /// 签名方式
        /// </summary>
        public static string SignType = "RSA2";

        /// <summary>
        /// 编码格式
        /// </summary>
        public static string CharSet = "UTF-8";
    }
}
