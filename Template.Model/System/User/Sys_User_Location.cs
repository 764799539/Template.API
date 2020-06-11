using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Model
{
    /// <summary>
    /// 用户位置信息
    /// </summary>
    public class Sys_User_Location : BaseEntity<Sys_User_Location>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public string Nationality { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 行政区
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 具体地址
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Latitude { get; set; }

    }
}
