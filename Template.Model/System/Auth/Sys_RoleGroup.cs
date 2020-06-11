using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Model
{
    /// <summary>
    /// 角色组关系
    /// </summary>
    public class Sys_RoleGroup : BaseEntity<Sys_RoleGroup>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleID { get; set; }
        /// <summary>
        /// 组ID
        /// </summary>
        public long GroupID { get; set; }
    }
}
