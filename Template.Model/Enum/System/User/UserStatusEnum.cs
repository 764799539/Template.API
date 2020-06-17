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
        /// <summary>
        /// 
        /// </summary>
        [Description("正常")]
        Normal = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("封禁")]
        Ban = -90,
        /// <summary>
        /// 
        /// </summary>
        [Description("删除")]
        Delete = -99,
    }
}
