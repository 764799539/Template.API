using Chloe.Annotations;

namespace Template.Model
{
    /// <summary>
    /// 角色权限关系
    /// </summary>
    public class Sys_RoleAuth : BaseEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleID { get; set; }
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
