using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using Template.BLL;
using Template.Model;
using Template.NuGet;

namespace Template.API.Controllers
{
    /// <summary>
    /// 系统API
    /// </summary>
    [ApiController]
    [Route("API/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;
        private readonly IAuthService _authService;

        /// <summary>
        /// 授权服务注入
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="commonService"></param>
        /// <param name="authService"></param>
        public AuthController(IUserService userService, ICommonService commonService,IAuthService authService)
        {
            _userService = userService;
            _commonService = commonService;
            _authService = authService;
        }

        #region 权限
        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="entity">权限实体</param>
        /// <returns></returns>
        [HttpPost, Route("SaveAuth")]
        [Authorization("Auth_Save")]
        public JsonReturn<bool> SaveAuth(Sys_Auth entity)
        {
            if (entity.ID <= 0)
            {
                if (!_commonService.IsExist<Sys_Auth>(sa => sa.Name == entity.Name && !sa.IsDelete))
                {
                    entity.CreateBy = AuthManager.UserID;
                    entity.CreateDate = DateTime.Now;
                    _commonService.Insert(entity);
                }
            }
            else
            {
                if (!_commonService.IsExist<Sys_Auth>(sa => sa.Name == entity.Name && !sa.IsDelete))
                {
                    _commonService.Update<Sys_Auth>(sa => sa.ID == entity.ID, sa => new Sys_Auth()
                    {
                        UpdateBy = AuthManager.UserID,
                        UpdateDate = DateTime.Now,
                        Type = entity.Type,
                        ParentID = entity.ParentID,
                        Describe = entity.Describe,
                        Name = entity.Name
                    });
                }
            }
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = true };
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="ID">权限ID</param>
        /// <returns></returns>
        [HttpPost, Route("DeleteAuth")]
        [Authorization("Auth_Del")]
        public JsonReturn<bool> DeleteAuth(long ID)
        {
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = _commonService.Update<Sys_Auth>(sa => sa.ID == ID, sa => new Sys_Auth() { IsDelete = true }) == 1, Msg = ""};
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="searchParam">查询参数</param>
        /// <param name="pagingParam">分页参数</param>
        /// <param name="sortingParam">排序参数</param>
        /// <returns></returns>
        [HttpPost, Route("GetAuthList")]
        [Authorization("Auth_Search")]
        public JsonReturn<PagedData<Sys_Auth>> GetAuthList(AuthSearchParam searchParam,PagingParam pagingParam, SortingParam sortingParam) {
            return new JsonReturn<PagedData<Sys_Auth>> { Status = ResultStatus.OK, Data = _authService.GetAuthList(searchParam, pagingParam, sortingParam), Msg = "" };
        }
        #endregion

        #region 角色
        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="entity">权限实体</param>
        /// <returns></returns>
        [HttpPost, Route("SaveRole")]
        [Authorization("Role_Save")]
        public JsonReturn<bool> SaveRole(Sys_Role entity)
        {
            if (entity.ID <= 0)
            {
                if (!_commonService.IsExist<Sys_Role>(sa => sa.Name == entity.Name && !sa.IsDelete))
                {
                    entity.CreateBy = AuthManager.UserID;
                    entity.CreateDate = DateTime.Now;
                    _commonService.Insert(entity);
                }
            }
            else
            {
                if (!_commonService.IsExist<Sys_Role>(sa => sa.Name == entity.Name && !sa.IsDelete))
                {
                    _commonService.Update<Sys_Role>(sa => sa.ID == entity.ID, sa => new Sys_Role()
                    {
                        UpdateBy = AuthManager.UserID,
                        UpdateDate = DateTime.Now,
                        ParentID = entity.ParentID,
                        Describe = entity.Describe,
                        Name = entity.Name
                    });
                }
            }
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = true };
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ID">角色ID</param>
        /// <returns></returns>
        [HttpPost, Route("DeleteRole")]
        [Authorization("Role_Del")]
        public JsonReturn<bool> DeleteRole(long ID)
        {
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = _commonService.Update<Sys_Role>(sa => sa.ID == ID, sa => new Sys_Role() { IsDelete = true }) == 1, Msg = "" };
        }


        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="searchParam">查询参数</param>
        /// <param name="pagingParam">分页参数</param>
        /// <param name="sortingParam">排序参数</param>
        /// <returns></returns>
        [HttpPost, Route("GetRoleList")]
        [Authorization("Role_Search")]
        public JsonReturn<PagedData<Sys_Role>> GetRoleList(RoleSearchParam searchParam, PagingParam pagingParam, SortingParam sortingParam)
        {
            return new JsonReturn<PagedData<Sys_Role>> { Status = ResultStatus.OK, Data = _authService.GetRoleList(searchParam, pagingParam, sortingParam), Msg = "" };
        }
        #endregion

        #region 组
        /// <summary>
        /// 保存组
        /// </summary>
        /// <param name="entity">组实体</param>
        /// <returns></returns>
        [HttpPost, Route("SaveGroup")]
        [Authorization("Group_Save")]
        public JsonReturn<bool> SaveGroup(Sys_Group entity)
        {
            if (entity.ID <= 0)
            {
                if (!_commonService.IsExist<Sys_Group>(sa => sa.Name == entity.Name && !sa.IsDelete))
                {
                    entity.CreateBy = AuthManager.UserID;
                    entity.CreateDate = DateTime.Now;
                    _commonService.Insert(entity);
                }
            }
            else
            {
                if (!_commonService.IsExist<Sys_Group>(sa => sa.Name == entity.Name && !sa.IsDelete))
                {
                    _commonService.Update<Sys_Group>(sa => sa.ID == entity.ID, sa => new Sys_Group()
                    {
                        UpdateBy = AuthManager.UserID,
                        UpdateDate = DateTime.Now,
                        ParentID = entity.ParentID,
                        Describe = entity.Describe,
                        Name = entity.Name
                    });
                }
            }
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = true };
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ID">组ID</param>
        /// <returns></returns>
        [HttpPost, Route("DeleteGroup")]
        [Authorization("Group_Del")]
        public JsonReturn<bool> DeleteGroup(long ID)
        {
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = _commonService.Update<Sys_Group>(sa => sa.ID == ID, sa => new Sys_Group() { IsDelete = true }) == 1, Msg = "" };
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="searchParam"></param>
        /// <param name="pagingParam"></param>
        /// <param name="sortingParam"></param>
        /// <returns></returns>
        [HttpPost, Route("GetGroupList")]
        [Authorization("Group_Search")]
        public JsonReturn<PagedData<Sys_Group>> GetGroupList(GroupSearchParam searchParam, PagingParam pagingParam, SortingParam sortingParam)
        {
            return new JsonReturn<PagedData<Sys_Group>> { Status = ResultStatus.OK, Data = _authService.GetGroupList(searchParam, pagingParam, sortingParam), Msg = "" };
        }
        #endregion

        #region 分配
        /// <summary>
        /// 用户分配角色
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="RoleID">角色ID</param>
        /// <returns></returns>
        [HttpPost, Route("SaveUserRole")]
        [Authorization("UserRole_Save")]
        public JsonReturn<bool> SaveUserRole(long UserID,long RoleID)
        {
            if (_commonService.GetCount<Sys_UserRole>(sa =>sa.UserID == UserID && sa.RoleID == RoleID && sa.IsDelete == false) > 0)
                return new JsonReturn<bool> { Status = ResultStatus.NotMeetRequirement, Data = true };
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = true };
        }
        #endregion

        #region 角色权限
        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <param name="Type">权限类型</param>
        /// <returns></returns>
        [HttpPost, Route("GetRoleAuthList")]
        [Authorization("RoleAuth_Search")]
        public JsonReturn<List<Sys_Auth>> GetRoleAuthList(long RoleID,AuthTypeEnum Type)
        {
            return new JsonReturn<List<Sys_Auth>> { Status = ResultStatus.OK, Data = _authService.GetRoleAuthList(RoleID, Type), Msg = "" };
        }
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <param name="AddAuthIDList">增加的权限ID列表</param>
        /// <param name="DelAuthIDList">减少的权限ID列表</param>
        /// <returns></returns>
        [HttpPost, Route("SaveRoleAuth")]
        [Authorization("RoleAuth_Save")]
        public JsonReturn<bool> SaveRoleAuth(long RoleID, List<long> AddAuthIDList, List<long> DelAuthIDList)
        {
            AddAuthIDList.ForEach(item =>
            {
                _commonService.Insert(new Sys_RoleAuth()
                {
                    AuthID = item,
                    RoleID = RoleID,
                    CreateBy = AuthManager.UserID,
                    CreateDate = DateTime.Now
                });
            });
            _commonService.Update<Sys_RoleAuth>(sa => sa.RoleID == RoleID && DelAuthIDList.Contains(sa.AuthID), sa => new Sys_RoleAuth() { IsDelete = true });
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = true, Msg = "" };
        }
        #endregion

        #region 组权限
        /// <summary>
        /// 获取组权限列表
        /// </summary>
        /// <param name="GroupID">组ID</param>
        /// <param name="Type">权限类型</param>
        /// <returns></returns>
        [HttpPost, Route("GetGroupAuthList")]
        [Authorization("GroupAuth_Search")]
        public JsonReturn<List<Sys_Auth>> GetGroupAuthList(long GroupID, AuthTypeEnum Type)
        {
            return new JsonReturn<List<Sys_Auth>> { Status = ResultStatus.OK, Data = _authService.GetGroupAuthList(GroupID, Type), Msg = "" };
        }
        /// <summary>
        /// 保存组权限
        /// </summary>
        /// <param name="GroupID">组ID</param>
        /// <param name="AddAuthIDList">增加的权限ID列表</param>
        /// <param name="DelAuthIDList">减少的权限ID列表</param>
        /// <returns></returns>
        [HttpPost, Route("SaveGroupAuth")]
        [Authorization("GroupAuth_Save")]
        public JsonReturn<bool> SaveGroupAuth(long GroupID, List<long> AddAuthIDList, List<long> DelAuthIDList)
        {
            AddAuthIDList.ForEach(item =>
            {
                _commonService.Insert(new Sys_GroupAuth()
                {
                    AuthID = item,
                    GroupID = GroupID,
                    CreateBy = AuthManager.UserID,
                    CreateDate = DateTime.Now
                });
            });
            _commonService.Update<Sys_GroupAuth>(sa => sa.GroupID == GroupID && DelAuthIDList.Contains(sa.AuthID), sa => new Sys_GroupAuth() { IsDelete = true });
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = true, Msg = "" };
        }
        #endregion

        #region 用户权限
        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Type">权限类型</param>
        /// <returns></returns>
        [HttpPost, Route("GetUserAuthList")]
        [Authorization("UserAuth_Search")]
        public JsonReturn<List<Sys_Auth>> GetUserAuthList(long UserID, AuthTypeEnum Type)
        {
            return new JsonReturn<List<Sys_Auth>> { Status = ResultStatus.OK, Data = _authService.GetUserAuthList(UserID, Type), Msg = "" };
        }
        /// <summary>
        /// 保存用户权限
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="AddAuthIDList">增加的权限ID列表</param>
        /// <param name="DelAuthIDList">减少的权限ID列表</param>
        /// <returns></returns>
        [HttpPost, Route("SaveUserAuth")]
        [Authorization("UserAuth_Save")]
        public JsonReturn<bool> SaveUserAuth(long UserID, List<long> AddAuthIDList, List<long> DelAuthIDList)
        {
            AddAuthIDList.ForEach(item =>
            {
                _commonService.Insert(new Sys_UserAuth()
                {
                    AuthID = item,
                    UserID = UserID,
                    CreateBy = AuthManager.UserID,
                    CreateDate = DateTime.Now
                });
            });
            _commonService.Update<Sys_UserAuth>(sa => sa.UserID == UserID && DelAuthIDList.Contains(sa.AuthID), sa => new Sys_UserAuth() { IsDelete = true });
            return new JsonReturn<bool> { Status = ResultStatus.OK, Data = true, Msg = "" };
        }
        #endregion
    }
}
