using Chloe;
using System.Collections.Generic;
using Template.Model;
using Template.NuGet;

namespace Template.BLL
{
    public class AuthService : BaseService, IAuthService
    {
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="searchParam">查询参数</param>
        /// <returns></returns>
        public PagedData<Sys_Auth> GetAuthList(AuthSearchParam searchParam)
        {
            return ReadDbContext.Query<Sys_Auth>(sa => sa.IsDelete == false && sa.Name.Contains(searchParam.Name) && sa.Describe.Contains(searchParam.Describe)).TakePageData(searchParam);
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="searchParam">查询参数</param>
        /// <returns></returns>
        public PagedData<Sys_Role> GetRoleList(RoleSearchParam searchParam)
        {
            return ReadDbContext.Query<Sys_Role>(sa => sa.IsDelete == false && sa.Name.Contains(searchParam.Name) && sa.Describe.Contains(searchParam.Describe)).TakePageData(searchParam);
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="searchParam"></param>
        /// <returns></returns>
        public PagedData<Sys_Group> GetGroupList(GroupSearchParam searchParam)
        {
            return ReadDbContext.Query<Sys_Group>(sa => sa.IsDelete == false && sa.Name.Contains(searchParam.Name) && sa.Describe.Contains(searchParam.Describe)).TakePageData(searchParam);
        }

        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <param name="Type">权限类型</param>
        /// <returns></returns>
        public List<Sys_Auth> GetRoleAuthList(long RoleID, AuthTypeEnum Type)
        {
            return ReadDbContext.JoinQuery<Sys_RoleAuth, Sys_Auth>((RoleAuth, Auth) => new object[]
             {
                JoinType.InnerJoin,RoleAuth.AuthID == Auth.ID
             }).Where((RoleAuth, Auth) => RoleAuth.RoleID == RoleID && RoleAuth.IsDelete == false && Auth.IsDelete == false).Select((RoleAuth, Auth) => Auth).ToList();
        }

        /// <summary>
        /// 获取组权限列表
        /// </summary>
        /// <param name="GroupID">组ID</param>
        /// <param name="Type">权限类型</param>
        /// <returns>角色权限列表</returns>
        public List<Sys_Auth> GetGroupAuthList(long GroupID, AuthTypeEnum Type)
        {
            return ReadDbContext.JoinQuery<Sys_GroupAuth, Sys_Auth>((GroupAuth, Auth) => new object[]
             {
                JoinType.InnerJoin,GroupAuth.AuthID == Auth.ID
             }).Where((GroupAuth, Auth) => GroupAuth.GroupID == GroupID && GroupAuth.IsDelete == false && Auth.IsDelete == false).Select((GroupAuth, Auth) => Auth).ToList();
        }
        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Type">权限类型</param>
        /// <returns></returns>
        public List<Sys_Auth> GetUserAuthList(long UserID, AuthTypeEnum Type)
        {
            return ReadDbContext.JoinQuery<Sys_UserAuth, Sys_Auth>((UserAuth, Auth) => new object[]
             {
                JoinType.InnerJoin,UserAuth.AuthID == Auth.ID
             }).Where((UserAuth, Auth) => UserAuth.UserID == UserID && UserAuth.IsDelete == false && Auth.IsDelete == false).Select((UserAuth, Auth) => Auth).ToList();
        }
    }
}
