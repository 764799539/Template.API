using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Model
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    public class Sys_UserRole : BaseEntity<Sys_UserRole>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleID { get; set; }
    }
}
