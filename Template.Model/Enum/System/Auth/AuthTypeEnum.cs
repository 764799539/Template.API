using System.ComponentModel;

namespace Template.Model
{
    /// <summary>
    /// 授权类型枚举
    /// </summary>
    public enum AuthTypeEnum
    {
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu = 1,
        /// <summary>
        /// 功能
        /// </summary>
        [Description("功能")]
        Function = 2,
    }
}
