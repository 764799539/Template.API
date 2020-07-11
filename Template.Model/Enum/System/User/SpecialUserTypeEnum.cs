using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace Template.Model
{
    /// <summary>
    /// 特殊用户类型枚举类
    /// </summary>
    public enum SpecialUserTypeEnum
    {
        /// <summary>
        /// 系统管理帐号
        /// </summary>
        [Description("系统管理帐号")]
        Master = 1,
        /// <summary>
        /// 广告投放商帐号
        /// </summary>
        [Description("广告投放商帐号")]
        Advertisement = 2
    }
}
