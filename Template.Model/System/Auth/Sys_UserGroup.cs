using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Model
{
    /// <summary>
    /// 用户组关系
    /// </summary>
    public class Sys_UserGroup : BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 组ID
        /// </summary>
        public long GroupID { get; set; }
    }
}
