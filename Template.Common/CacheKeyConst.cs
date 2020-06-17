using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Common
{
    public static class CacheKeyConst
    {
        #region 晒图标签缓存项
        /// <summary>
        /// 晒图标签缓存
        /// </summary>
        public const string AllShowLabel = "AllShowLabel";
        #endregion

        #region 系统缓存项
        /// <summary>
        /// 登录用户Token缓存
        /// </summary>
        public const string UserToken = "UserToken";
        /// <summary>
        /// 用户用例编码列表缓存
        /// </summary>
        public const string UsecaseCodeList = "UsecaseCodeList";
        /// <summary>
        /// 系统全局配置缓存
        /// </summary>
        public const string GlobalConfig = "GlobalConfig";
        /// <summary>
        /// 系统菜单列表缓存
        /// </summary>
        public const string AllModule = "AllModule";
        /// <summary>
        /// 业务系统列表缓存
        /// </summary>
        public const string AppList = "AppList";
        /// <summary>
        /// 所有有效广告列表缓存
        /// </summary>
        public const string AllAdvert = "AllAdvert";
        /// <summary>
        /// 所有有效公告列表缓存
        /// </summary>
        public const string AllNotice = "AllNotice";
        /// <summary>
        /// 秒杀队列缓存
        /// </summary>
        public const string SecondsQueue = "SecondsQueue";
        /// <summary>
        /// 能量值排行榜缓存
        /// </summary>
        public const string TopBuyNumList = "TopBuyNumList";
        /// <summary>
        /// 推荐达人排行榜缓存
        /// </summary>
        public const string TopReferrerList = "TopReferrerList";
        /// <summary>
        /// 奖金排行榜缓存
        /// </summary>
        public const string TopBonusList = "TopBonusList";
        /// <summary>
        /// 热门晒图
        /// </summary>
		public const string HotShowImages = "HotShowImages";
        /// <summary>
        /// 抽奖
        /// </summary>
        public const string AllLottery = "AllLottery";
        /// <summary>
        /// 优惠卷
        /// </summary>
        public const string AllCoupon = "AllCoupon";
        /// <summary>
        /// 优惠卷金额规则
        /// </summary>
        public const string AllCouponAmount = "AllCouponAmount";
        /// <summary>
        /// 优惠卷发布
        /// </summary>
        public const string AllCouponPublish = "AllCouponPublish";
        /// <summary>
        /// 优惠卷组合
        /// </summary>
        public const string AllCouponGroup = "AllCouponGroup";
        #endregion

        #region 产品缓存项
        /// <summary>
        /// 全部有效商品品牌
        /// </summary>
        public const string AllProductBrand = "AllProductBrand";
        /// <summary>
        /// 全部有效商品分类
        /// </summary>
        public const string AllProductClass = "AllProductClass";
        /// <summary>
        /// 全部商品列表
        /// </summary>
        public const string AllProduct = "AllProduct";
        /// <summary>
        /// 全部商品价格列表
        /// </summary>
        public const string AllProductPrice = "AllProductPrice";
        /// <summary>
        /// 商品晒图SortedSet Key
        /// </summary>
        public static readonly string ProductPicShowSortedSetKey = "PicShowSortedSet";
        /// <summary>
        /// 商品晒图Hash Key
        /// </summary>
        public static readonly string ProductPicShowHashKey = "PicShowHash";
        /// <summary>
        /// 商品缓存
        /// </summary>
        public const string ProductStockKey = "ProductStockKey";
        /// <summary>
        /// 物流公司
        /// </summary>
        public const string LogisticsCompany = "LogisticsCompany";
        /// <summary>
        /// 运费信息
        /// </summary>
		public const string AllLogisticsFee = "AllLogisticsFee";
        /// <summary>
        /// 团购价格
        /// </summary>
        public const string AllGroupPrice = "AllGroupPrice";
        #endregion

        #region 会员缓存项
        /// <summary>
        /// 全部会员等级列表
        /// </summary>
        public const string AllMemberLevel = "AllMemberLevel";

        /// <summary>
        /// 全部活跃等级列表
        /// </summary>
        public const string AllActiveLevel = "AllActiveLevel";
        #endregion

        #region 红包缓存项
        /// <summary>
        /// 全部红包
        /// </summary>
        public const string AllRedPacket = "AllRedPacket";
        /// <summary>
        /// 全部红包金额规则
        /// </summary>
        public const string AllRedPacketRule = "AllRedPacketRule";
        /// <summary>
        /// 全部红包发布
        /// </summary>
        public const string AllRedPacketSend = "AllRedPacketSend";
        #endregion

        #region 支付类型缓存项
        /// <summary>
        /// 支付宝支付
        /// </summary>
        public const string AliPayResult = "AliPayResult";
        /// <summary>
        /// 微信APP支付
        /// </summary>
		public const string WxAppPayResult = "WxAppPayResult";
        /// <summary>
        /// 微信公众号支付
        /// </summary>
        public const string WxH5PayResult = "WxH5PayResult";
        #endregion

        #region 日志类型项
        /// <summary>
        /// 登录日志
        /// </summary>
        public const string LoginLog = "LoginLog";
        /// <summary>
        /// 操作日志
        /// </summary>
        public const string OperateLog = "OperateLog";
        /// <summary>
        /// 异常日志
        /// </summary>
        public const string ExceptionLog = "ExceptionLog";
        #endregion

        /// <summary>
        /// 搜索热点
        /// </summary>
		public const string AllSearchSpot = "AllSearchSpot";

        /// <summary>
        /// 城市
        /// </summary>
        public const string AllCity = "AllCity";

        /// <summary>
        /// 点赞
        /// </summary>
        public const string AllTags = "AllTags";

        /// <summary>
        /// 客服
        /// </summary>
        public const string AllService = "AllService";

        /// <summary>
        /// 不在线客服
        /// </summary>
        public const string OutLineService = "OutLineService";

        /// <summary>
        /// 黑名单
        /// </summary>
		public const string BlackList = "BlackList";

        /// <summary>
        /// 库存预警商品
        /// </summary>
        public const string AllWarnStockProduct = "AllWarnStockProduct";

        /// <summary>
        /// 春节滚动红包
        /// </summary>
        public const string RedPacketRollList = "RedPacketRollList";
    }
}
