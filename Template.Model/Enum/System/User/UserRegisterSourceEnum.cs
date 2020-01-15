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
        [Description("用户名")]
        UserName = 1,
        [Description("手机号")]
        Mobile = 2,
        [Description("邮箱")]
        Email = 3,
        [Description("QQ")]
        QQ = 4,
        [Description("微信")]
        WeChat = 5,
        [Description("腾讯微博")]
        TencentMicroBlog = 6,
        [Description("新浪微博")]
        SinaMicroBlog = 7
    }
}
