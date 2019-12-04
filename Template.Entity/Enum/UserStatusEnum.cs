using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Template.Model
{
    /// <summary>
    /// 用户状态枚举类
    /// </summary>
    public enum UserStatusEnum
    {
        [Description("正常")]
        Normal = 0,
        [Description("封禁")]
        Ban = -90,
        [Description("删除")]
        Delete = -99,
    }
}
