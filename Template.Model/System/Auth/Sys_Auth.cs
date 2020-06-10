using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Model.System.Auth
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Sys_Auth : BaseEntity<Sys_User>
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public long ParentID { get; set; }
        /// <summary>
        /// 类型(1菜单 2按钮/功能)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
    }
}
