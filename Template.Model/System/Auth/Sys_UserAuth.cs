using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Model
{
    /// <summary>
    /// 用户权限关系
    /// </summary>
    public class Sys_UserAuth : BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public long AuthID { get; set; }
    }
}
