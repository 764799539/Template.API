using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Template.Model
{
    /// <summary>
    /// 用户注册来源枚举类
    /// </summary>
    public enum UserRegisterSourceEnum
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("用户名")]
        UserName = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("手机号")]
        Mobile = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("邮箱")]
        Email = 3,
        /// <summary>
        /// 
        /// </summary>
        [Description("QQ")]
        QQ = 4,
        /// <summary>
        /// 
        /// </summary>
        [Description("微信")]
        WeChat = 5,
        /// <summary>
        /// 
        /// </summary>
        [Description("腾讯微博")]
        TencentMicroBlog = 6,
        /// <summary>
        /// 
        /// </summary>
        [Description("新浪微博")]
        SinaMicroBlog = 7
    }
}
