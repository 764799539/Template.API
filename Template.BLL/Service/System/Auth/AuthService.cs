using Chloe;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Model;
using Template.NuGet;

namespace Template.BLL
{
    public class AuthService : BaseService, IAuthService
    {
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <param name="Type">权限类型</param>
        /// <returns></returns>
        public List<Sys_RoleAuth> GetRoleAuthList(long RoleID, AuthTypeEnum Type) 
        {
            return ReadDbContext.JoinQuery<Sys_RoleAuth, Sys_Auth>((RoleAuth, Auth) => new object[]
             {
                JoinType.InnerJoin,RoleAuth.AuthID == Auth.ID
             }).Select((RoleAuth, Auth) => new Sys_RoleAuth
             {
                 ID = RoleAuth.ID,
                 AuthID = RoleAuth.AuthID,
                 RoleID = RoleAuth.RoleID,
                 Auth = Auth,
                 IsDelete = RoleAuth.IsDelete
             }).Where(sa => sa.IsDelete == false).ToList();
        }
    }
}
