using System;
using System.Collections.Generic;
using System.Text;
using Chloe.Annotations;
using Template.NuGet;


namespace Template.Model
{
    public class Sys_User_LoginAuth : BaseEntity<Sys_User_LoginAuth>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 授权标识符
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// 授权凭证
        /// </summary>
        public string Certificate { get; set; }
        /// <summary>
        /// 标识符类型(1用户名 2手机号 3邮箱 4QQ 5微信 6腾讯微博 7新浪微博)
        /// </summary>
        public int Type { get; set; }
    }
}
