using Chloe.Annotations;
using System;

using Template.NuGet;

namespace Template.Model
{
    public class BaseEntity
    {
        /// <summary>
        /// SnowFlakeID
        /// </summary>
        [NonAutoIncrement] // 非数据库自增主键标记
        public long ID
        {
            get => ID;
            set
            {
                if (value == 0)
                    ID = new SnowFlakeHelper(0, 0).NextId();
                else
                    ID = value;
            }
        }

        /// <summary>
        /// 状态(0正常)
        /// </summary>
        public int? Status
        {
            get => Status == null ? 0 : Status;
            set => Status = value;
        }

        /// <summary>
        /// 是否删除(0否)
        /// </summary>
        public int? IsDelete
        {
            get => IsDelete == null ? 0 : IsDelete;
            set
            {
                if (value == 1 || value == 0)
                    IsDelete = value;
                else
                    throw new Exception("是否删除IsDelete的值只能是0或1");
            }
        }

        /// <summary>
        /// 乐观锁
        /// </summary>
        public int? Revision
        {
            get => Revision == null ? 1 : Revision;
            set
            {
                if (Revision != null && value <= Revision)
                    throw new Exception("乐观锁Revision的值只能增加！");
                else
                    Revision = value;
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public long? CreateBy
        {
            get => CreateBy == null ? 00000000000000000000L : CreateBy;
            set => CreateBy = value;
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate
        {
            get => CreateDate == null ? DateTime.Now : CreateDate;
            set => CreateDate = value;
        }

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
