using System;
using System.Collections.Generic;
using System.Text;
using Template.NuGet;
using Template.Model;

namespace Template.BLL
{
    public interface IAuthService : IBaseService
    {
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <param name="Type">权限类型</param>
        /// <returns></returns>
        List<Sys_RoleAuth> GetRoleAuthList(long RoleID, AuthTypeEnum Type);
    }
}
