using Chloe.Annotations;
using System;
using Template.NuGet;

namespace Template.Model
{
    public class Sys_User : BaseEntity<Sys_User>
    {
        ///// <summary>
        ///// SnowFlakeID
        ///// </summary>
        //[NonAutoIncrement] //非数据库自增主键标记
        //public long ID
        //{
        //    get => ID;
        //    set
        //    {
        //        if (value == 0)
        //            ID = new SnowFlakeHelper(0, 0).NextId();
        //        else
        //            ID = value;
        //    }
        //}
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentifyCode { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 性别(0男 1女)
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 注册来源(1用户名 2手机号 3邮箱 4QQ 5微信 6腾讯微博 7新浪微博)
        /// </summary>
        public int RegisterSource { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 个人签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string FaceImg { get; set; }
        /// <summary>
        /// 特殊用户类型(1系统管理帐号 2广告投放商帐号)
        /// </summary>
        public int? SpecialType { get; set; }
        ///// <summary>
        ///// 状态(0正常 -90封禁)
        ///// </summary>
        //public int? Status { get; set; }

        ///// <summary>
        ///// 是否删除(0否1是)
        ///// </summary>
        //public int? IsDelete
        //{
        //    get => IsDelete == null ? 0 : IsDelete;
        //    set
        //    {
        //        if (value == 1 || value == 0)
        //            IsDelete = value;
        //        else
        //            throw new Exception("是否删除IsDelete的值只能是0或1");
        //    }
        //}

        ///// <summary>
        ///// 乐观锁
        ///// </summary>
        //public int? Revision
        //{
        //    get => Revision == null ? 1 : Revision;
        //    set
        //    {
        //        if (Revision != null && value <= Revision)
        //            throw new Exception("乐观锁Revision的值只能增加！");
        //        else
        //            Revision = value;
        //    }
        //}

        ///// <summary>
        ///// 创建人
        ///// </summary>
        //public long? CreateBy
        //{
        //    get => CreateBy == null ? 00000000000000000000L : CreateBy;
        //    set => CreateBy = value;
        //}

        ///// <summary>
        ///// 创建日期
        ///// </summary>
        //public DateTime? CreateDate
        //{
        //    get => CreateDate == null ? DateTime.Now : CreateDate;
        //    set => CreateDate = value;
        //}

        ///// <summary>
        ///// 更新人
        ///// </summary>
        //public long? UpdateBy { get; set; }

        ///// <summary>
        ///// 更新时间
        ///// </summary>
        //public DateTime? UpdateDate { get; set; }
    }
}
