using Chloe.Annotations;
using Chloe.Entity;
using System;
using System.Linq.Expressions;
using System.Reflection;
using Template.NuGet;

namespace Template.Model
{
    public class BaseEntity
    {
        /// <summary>
        /// SnowFlakeID
        /// </summary>
        [NonAutoIncrement] // 非数据库自增主键标记
        public long ID { get; set; }

        /// <summary>
        /// 状态(0正常)
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 是否删除(0否)
        /// </summary>
        public int? IsDelete { get; set; }

        /// <summary>
        /// 乐观锁
        /// </summary>
        public int? Revision { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long? CreateBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public long? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }
}
