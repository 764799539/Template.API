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
        /// 获取权限列表
        /// </summary>
        /// <param name="pagingParam">分页参数</param>
        /// <param name="sortingParam">排序参数</param>
        /// <returns></returns>
        PagedData<Sys_Auth> GetAuthList(AuthSearchParam searchParam, PagingParam pagingParam, SortingParam sortingParam);
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="searchParam">查询参数</param>
        /// <param name="pagingParam">分页参数</param>
        /// <param name="sortingParam">排序参数</param>
        /// <returns></returns>
        PagedData<Sys_Role> GetRoleList(RoleSearchParam searchParam, PagingParam pagingParam, SortingParam sortingParam);
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="searchParam"></param>
        /// <param name="pagingParam"></param>
        /// <param name="sortingParam"></param>
        /// <returns></returns>
        PagedData<Sys_Group> GetGroupList(GroupSearchParam searchParam, PagingParam pagingParam, SortingParam sortingParam);
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <param name="Type">权限类型</param>
        /// <returns>角色权限列表</returns>
        List<Sys_Auth> GetRoleAuthList(long RoleID, AuthTypeEnum Type);
        /// <summary>
        /// 获取组权限列表
        /// </summary>
        /// <param name="RoleID">组ID</param>
        /// <param name="Type">权限类型</param>
        /// <returns>角色权限列表</returns>
        List<Sys_Auth> GetGroupAuthList(long RoleID, AuthTypeEnum Type);
        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Type">权限类型</param>
        /// <returns></returns>
        List<Sys_Auth> GetUserAuthList(long UserID, AuthTypeEnum Type);
    }
}
