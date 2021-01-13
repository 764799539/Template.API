using System;
using System.Collections.Generic;
using System.Text;
using Template.NuGet;

namespace Template.Model
{
    /// <summary>
    /// 角色查询参数
    /// </summary>
    public class RoleSearchParam : PagingAndSortingParam
    {
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
