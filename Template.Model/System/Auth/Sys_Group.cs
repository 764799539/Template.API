﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Model
{
    /// <summary>
    /// 组
    /// </summary>
    public class Sys_Group : BaseEntity
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public long ParentID { get; set; }
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
