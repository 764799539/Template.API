using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Model
{
    /// <summary>
    /// 组权限关系
    /// </summary>
    public class Sys_GroupAuth : BaseEntity
    {
        /// <summary>
        /// 组ID
        /// </summary>
        public long GroupID { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public long AuthID { get; set; }
    }
}
