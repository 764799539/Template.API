using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.NuGet;

namespace Template.Model
{
    /// <summary>
    /// 授权查询参数
    /// </summary>
    public class AuthSearchParam : PagingAndSortingParam
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
