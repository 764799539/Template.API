using Chloe.Annotations;
using System;
using Template.NuGet;

namespace Template.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    public class Sys_User : BaseEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentifyCode { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 性别(0男 1女)
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 注册来源(1用户名 2手机号 3邮箱 4QQ 5微信 6腾讯微博 7新浪微博)
        /// </summary>
        public int RegisterSource { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 个人签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string FaceImg { get; set; }
        /// <summary>
        /// 特殊用户类型(1系统管理帐号 2广告投放商帐号)
        /// </summary>
        public int? SpecialType { get; set; }
        /// <summary>
        /// 用户当前Token
        /// </summary>
        [NotMapped]
        public string Token { get; set; }
    }
}
