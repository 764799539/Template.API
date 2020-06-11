using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Model
{
    /// <summary>
    /// 角色权限关系
    /// </summary>
    public class Sys_RoleAuth : BaseEntity<Sys_RoleAuth>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleID { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public long AuthID { get; set; }
    }
}
