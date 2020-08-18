using Chloe.Annotations;

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

        /// <summary>
        /// 权限对象
        /// </summary>
        [NotMapped]
        public Sys_Auth Auth { get; set; }
    }
}
